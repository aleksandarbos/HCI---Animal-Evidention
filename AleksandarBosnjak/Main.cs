using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace AleksandarBosnjak
{
    public partial class Main : Form
    {

        public static int selectedId;
        public static int alpha = 170;
        private Model model;
        private bool dozvoljenDragDrop;
        private bool selektovan = false;
        private ContextMenuStrip stikerContextMenu = new ContextMenuStrip();
        private StikerView aktivanStikerView = new StikerView();
        private String filterKolona = "";
        private bool dragEnterPanel9 = false;
        private DragEventArgs eDragDrop;

        public Main()
        {
            InitializeComponent();
            open();                                         // otvaram sacuvane podatke
            //this.model = new Model();
            initContextMenu();
            refreshed = false;
            aktivanStikerView.vrsta = model.vrste[0];
            
        }

        private void initContextMenu()
        {
            stikerContextMenu.Items.Add("Obriši", global::AleksandarBosnjak.Properties.Resources.obrisiEtiketu, stikerContextMenu_ItemClicked);

            stikerContextMenu.Items.Add("Izmeni", AleksandarBosnjak.Properties.Resources.izm, stikerContextMenu_IzmeniClicked);
        }

        private void stikerContextMenu_IzmeniClicked(object sender, EventArgs e)
        {
            button4_Click(new Object(), null);
            System.Diagnostics.Debug.WriteLine("IZMENI STICKER!");
        }


        private void stikerContextMenu_ItemClicked(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("sender: " + sender.ToString());
            int idxStiker = model.stikeriMapa.FindIndex(item => item.aktivnaEtiketa.oznaka == aktivanStikerView.vrsta.aktivnaEtiketa.oznaka);
            aktivanStikerView.Visible = false;
            model.stikeriMapa.RemoveAt(idxStiker);
            selektovan = false;
            panel9.Controls["pictureBox1"].Visible = false;
            //int i = 6;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DodavanjeVrste dodVrste = new DodavanjeVrste(model);
            DodavanjeVrste.MODE = "UNOS";
            dodVrste.ShowDialog(this);
        }

        private void Main_Activated(object sender, EventArgs e)
        {
           aktivanStikerView.bojaEtiketa.BackColor = Color.FromArgb(aktivanStikerView.vrsta.aktivnaEtiketa.boja);
            aktivanStikerView.naziv.Text = aktivanStikerView.vrsta.aktivnaEtiketa.oznaka;
            
          

                if (filterKolona.Equals(""))
                {

                    dataGridView1.Rows.Clear();

                    for (int i = 0; i < model.vrste.Count; i++)
                    {
                        Vrsta vrsta = model.vrste[i];

                        dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());

                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)  // selektovanje u tabeli poslednje aktivne etikete
                    {
                        String oznaka = null;
                        if (dataGridView1.Rows[i].Cells["Oznaka"].Value != null)
                            oznaka = (dataGridView1.Rows[i].Cells["Oznaka"].Value).ToString();
                        if (oznaka != null)
                        {
                            if (aktivanStikerView.vrsta.oznaka != null && aktivanStikerView.vrsta != null && aktivanStikerView != null) // nznm zasto...
                            {
                                if (((String)dataGridView1.Rows[i].Cells["Oznaka"].Value == aktivanStikerView.vrsta.oznaka))
                                {
                                    dataGridView1.ClearSelection();
                                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[i].Index;
                                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                                    dataGridView1.Rows[i].Selected = true;
                                    dataGridView1.Refresh();
                                }
                            }
                        }
                    }
                }
                else
                {  // filtriranje po kriterijumu!
                    dataGridView1.Rows.Clear();
                    Vrsta vrsta = new Vrsta();
                    for (int i = 0; i < model.vrste.Count; i++)
                    {
                        vrsta = model.vrste[i];
                        if (filterKolona.Equals("Naziv"))
                        {
                            if (vrsta.naziv.Contains(textBox1.Text))
                                dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());
                        }
                        else if (filterKolona.Equals("Oznaka"))
                        {
                            if (vrsta.oznaka.Contains(textBox1.Text))
                                dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());
                        }
                        else if (filterKolona.Equals("Opis"))
                        {
                            if (vrsta.opis.Contains(textBox1.Text))
                                dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());
                        }
                        else if (filterKolona.Equals("Tip vrste"))
                        {
                            if (vrsta.tipVrste.ime.Contains(textBox1.Text))
                                dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());
                        }
                        else if (filterKolona.Equals("Status ugroženosti"))
                        {
                            if (vrsta.statusUgrozenosti.Contains(textBox1.Text))
                                dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());
                        }
                        else if (filterKolona.Equals("Turistički status"))
                        {
                            if (vrsta.turistickiStatus.Contains(textBox1.Text))
                                dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());

                        }
                        else if (filterKolona.Equals("Urbana sredina"))
                        {
                            Boolean provera = textBox1.Text.Equals("DA");
                            if (vrsta.urbanaSredina == provera)
                                dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());
                        }
                        else if (filterKolona.Equals("Opasna po ljude"))
                        {
                            Boolean provera = textBox1.Text.Equals("DA");
                            if (vrsta.opasnaZaLjude == provera)
                                dataGridView1.Rows.Add(i + 1, vrsta.oznaka, vrsta.naziv, vrsta.opis, vrsta.tipVrste.ime, vrsta.statusUgrozenosti,
                                               vrsta.turistickiStatus, boolIspis(vrsta.urbanaSredina),
                                               boolIspis(vrsta.opasnaZaLjude), boolIspis(vrsta.iucnLista), vrsta.godisnjiPrihod, vrsta.datumOtkrivanja.ToShortDateString());
                        }

                    }
                }
        }

    private String boolIspis(Boolean izraz) {
        return (izraz) ? "DA" : "NE";
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
        DodavanjeTipa dodTip = new DodavanjeTipa(model);
        DodavanjeTipa.MODE = "UNOS";
        dodTip.ShowDialog();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        DodavanjeEtiketa dodEt = new DodavanjeEtiketa(model);
        DodavanjeEtiketa.MODE = "UNOS";
        dodEt.ShowDialog();
       
        Vrsta selVrst;
        try
        {
            selVrst = model.vrste[(selectedId - 1)];
        }
        catch (Exception)
        {

            throw;
        }

        dodEt.initData("Dodavanje nove etikete za: " + selVrst.naziv, 
                        selVrst.naziv, "", "", 0, "Dodaj novu etiketu", aktivanStikerView.vrsta.aktivnaEtiketa); 

    }

    private void button5_Click(object sender, EventArgs e)
    {
        DodavanjeVrste izmVrs = new DodavanjeVrste(model);
       
        Vrsta selVrst;
        
        try {
            selVrst = model.vrste[(selectedId - 1)];
        }
        catch (Exception) {

            throw;
        }
   

        DodavanjeVrste.MODE = "IZMENA";


        izmVrs.initData("Izmena vrste: " + selVrst.naziv,
                        "izmeniSticker.png", selVrst.naziv, selVrst.tipVrste.ime, selVrst.godisnjiPrihod,
                        selVrst.opasnaZaLjude, selVrst.iucnLista, selVrst.urbanaSredina, selVrst.statusUgrozenosti,                               
                        selVrst.turistickiStatus, selVrst.datumOtkrivanja, selVrst.oznaka,
                        selVrst.opis, selVrst.slicica, "Sačuvaj izmene", aktivanStikerView.vrsta.aktivnaEtiketa);

        izmVrs.ShowDialog();

        
    }

    private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        selectedId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
        System.Diagnostics.Debug.WriteLine("upravo sam selektovao neki novi red : " + model.vrste[Main.selectedId-1].tipVrste.slicica.Size.ToString());
        dataGridView1.Rows[e.RowIndex].Selected = true;

        if (model.vrste[selectedId - 1].aktivnaEtiketa.oznaka.Equals(""))
        {
            etiketaLabela.Text = model.vrste[selectedId - 1].naziv;
        }
        else {
            etiketaLabela.Text = model.vrste[selectedId - 1].aktivnaEtiketa.oznaka;
        }

        okvirBoja.BackColor = Color.FromArgb(alpha, Color.FromArgb(model.vrste[selectedId - 1].aktivnaEtiketa.boja));

        if (model.vrste[selectedId - 1].etikete.Count > 0)      // provera za slider levo desno ako ima sta da se menja
        {
            dozvoljenDragDrop = true;
            podesiDragDropKursor(true);
        }
        else
        {
            dozvoljenDragDrop = false;
            podesiDragDropKursor(false);
        }

        /*if (model.vrste[selectedId - 1].etikete.Count == 1 ||
            model.vrste[selectedId - 1].etikete.Count == 0) {
                levoDugme.Enabled = false;
                desnoDugme.Enabled = false;
        }*/

        if (model.vrste[selectedId - 1].etikete.Count <= 1)
        {
            levoDugme.Enabled = false;
            desnoDugme.Enabled = false;
        }
        else
        {
            levoDugme.Enabled = true;
            desnoDugme.Enabled = true;
        }


        if (model.vrste[selectedId - 1].etiketaCount == 0 && model.vrste[selectedId-1].etikete.Count > 1) {
            desnoDugme.Enabled = true;
            levoDugme.Enabled = false;
        }

        if (model.vrste[selectedId - 1].etiketaCount == model.vrste[selectedId - 1].etikete.Count )
        {
            desnoDugme.Enabled = false;
            levoDugme.Enabled = true;
        }

        if (model.vrste[selectedId - 1].aktivnaEtiketa.oznaka.Equals(""))
        {
            button4.Enabled = false;
            button7.Enabled = false;
            dozvoljenDragDrop = false;
            podesiDragDropKursor(false);
        }
        else
        {
            button4.Enabled = true;
            button7.Enabled = true;
            dozvoljenDragDrop = true;
            podesiDragDropKursor(true);
        }

        if (model.vrste[selectedId - 1].slicica == null)
        {
            etiketaSlika.Image = model.vrste[selectedId-1].tipVrste.slicica;
        }
        else {
            etiketaSlika.Image = model.vrste[selectedId - 1].slicica;
        }

       if(model.vrste[selectedId-1].etiketaCount == model.vrste[selectedId-1].etikete.Count) {
           desnoDugme.Enabled = false;
       }
    }

    private void podesiDragDropKursor(bool p)
    {
        if (!p)
        {
            panel8.Cursor = Cursors.Default;
            okvirBoja.Cursor = Cursors.Default;
            etiketaLabela.Cursor = Cursors.Default;
            etiketaSlika.Cursor = Cursors.Default;
            pictureBox2.Cursor = Cursors.Default;
        }
        else {
            panel8.Cursor = Cursors.NoMove2D;
            okvirBoja.Cursor = Cursors.NoMove2D;
            etiketaLabela.Cursor = Cursors.NoMove2D;
            etiketaSlika.Cursor = Cursors.NoMove2D;
            pictureBox2.Cursor = Cursors.NoMove2D;
        }
    }

    private void button6_Click(object sender, EventArgs e)      // izmeni tip btn
    {
        DodavanjeTipa izmTip = new DodavanjeTipa(model);
        Vrsta selVrst;

        try
        {
            selVrst = model.vrste[(selectedId - 1)];
        }
        catch (Exception)
        {

            throw;
        }


        DodavanjeTipa.MODE = "IZMENA";

        izmTip.Show();

        izmTip.initData("Izmena tipa: " + selVrst.tipVrste.ime,
                        selVrst.tipVrste.ime, selVrst.tipVrste.opis, "Sačuvaj izmene", selVrst.tipVrste.oznaka);
        //izmVrs.sacuvaj();

    }

    private void button4_Click(object sender, EventArgs e)
    {
        DodavanjeEtiketa izmEt = new DodavanjeEtiketa(model);
        Vrsta selVrst;

        try
        {
            selVrst = model.vrste[(selectedId - 1)];
        }
        catch (Exception)
        {

            throw;
        }


        DodavanjeEtiketa.MODE = "IZMENA";

        izmEt.Show();

        int vrstaIdx = 0, etiketaIdx = 0;

        for (int i = 0; i < model.vrste.Count; i++) {
            for (int j = 0; j < model.vrste[i].etikete.Count; j++) {
                if (model.vrste[i].etikete[j].oznaka.Equals(selVrst.aktivnaEtiketa.oznaka)) {
                    vrstaIdx = i;
                    etiketaIdx = j;
                    break;
                }
            }
            
            //break;
        }

        izmEt.initData("Izmena etikete: " + selVrst.etikete[etiketaIdx].oznaka, selVrst.aktivnaEtiketa.oznaka,
                       selVrst.etikete[etiketaIdx].oznaka, selVrst.etikete[etiketaIdx].opis, 
                       selVrst.etikete[etiketaIdx].boja, "Sacuvaj izmene", aktivanStikerView.vrsta.aktivnaEtiketa);

    }

    private void button8_Click(object sender, EventArgs e)      // obrisi vrstu
    {
        int idxSelect = Main.selectedId - 1;

        DialogResult rezultat = MessageBox.Show("Da li ste sigurni da zelite obrisati vrstu: " 
                        + model.vrste[Main.selectedId - 1].naziv + " ?", "Brisanje vrste",
                        MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
        if (rezultat == DialogResult.Yes)
        {
            model.vrste.RemoveAt(idxSelect);
            Main_Activated(new object(), new EventArgs());
        }
    }

    private void button9_Click(object sender, EventArgs e)      // obrisi tip vrste
    {
        int idxSelect = Main.selectedId - 1;
        String tipVrsteStari = model.vrste[idxSelect].tipVrste.ime;

        DialogResult rezultat = MessageBox.Show("Da li ste sigurni da zelite obrisati tip vrste: "
                        + model.vrste[Main.selectedId - 1].tipVrste.ime + " ?", "Brisanje tipa vrste",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
        
        if (rezultat == DialogResult.Yes)
        {
            Vrsta vr = model.vrste[selectedId-1];

            for (int k = 0; k < model.vrste.Count; k++) {
                if (model.vrste[k].tipVrste.ime.Equals(tipVrsteStari)) {
                    model.vrste.RemoveAt(k);
                    k--;
                }
            }

                for (int i = 0; i < model.tipoviVrste.Count; i++)
                {
                    if (model.tipoviVrste[i].ime.Equals(tipVrsteStari))
                    {
                        model.tipoviVrste.RemoveAt(i);          // obrisan iz mogucih odabira za vrstu
                        i--;
                    }
                }


//            model.vrste[idxSelect].tipVrste = new TipVrste("", Image.FromFile("nijePronadjena.png"), "", "");

            Main_Activated(new object(), new EventArgs());
        }
    }

    private void button7_Click(object sender, EventArgs e)
    {
        int idxSelect = Main.selectedId - 1;
        
        DialogResult rezultat = MessageBox.Show("Da li ste sigurni da zelite obrisati etiketu: "
                        + model.vrste[Main.selectedId - 1].aktivnaEtiketa.oznaka + " ?", "Brisanje etikete",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

        if (rezultat == DialogResult.Yes)
        {
            int i;
            for (i = 0; i < model.vrste[idxSelect].etikete.Count; i++)
            {
                if (model.vrste[idxSelect].aktivnaEtiketa.oznaka.Equals(model.vrste[idxSelect].etikete[i].oznaka))
                    break;
            }

            model.vrste[idxSelect].etikete.RemoveAt(i);          // obrisan iz mogucih odabira za vrstu
            model.vrste[idxSelect].aktivnaEtiketa = new Etiketa("", 0, "");

            Main_Activated(new object(), new EventArgs());
        }
    }


    public void save()
    {

        FileStream fs = new FileStream("DataFile.dat", FileMode.Create);
        // Construct a BinaryFormatter and use it to serialize the data to the stream.
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, model);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    public void open()
    {

        // Open the file containing the data that you want to deserialize.
        FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            // Deserialize the hashtable from the file and  
            // assign the reference to the local variable.
            this.model = (Model)formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

        // To prove that the table deserialized correctly,  
        // display the key/value pairs. 



    private void button10_Click(object sender, EventArgs e)     // sacuvavanje podataka
    {
        save();
    }

    private void dataGridView1_Resize(object sender, EventArgs e)
    {
        //System.Diagnostics.Debug.WriteLine("resize tabele!!!!!!!!!!!!!!!");
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void Main_Load(object sender, EventArgs e)
    { 
        foreach (Vrsta v in model.stikeriMapa) {
                StikerView sv = new StikerView(v);
                sv.Location = new Point(v.aktivnaEtiketa.x, v.aktivnaEtiketa.y);

                sv.MouseDown += sv_MouseDown;
                sv.Controls["naziv"].MouseDown += sv_MouseDown;
                sv.Controls["bojaEtiketa"].MouseDown += sv_MouseDown;
                sv.Controls["slikaEtiketa"].MouseDown += sv_MouseDown;

                sv.MouseUp += sv_MouseUp;
                sv.Controls["naziv"].MouseUp += sv_MouseUp;
                sv.Controls["bojaEtiketa"].MouseUp += sv_MouseUp;
                sv.Controls["slikaEtiketa"].MouseUp += sv_MouseUp;

                /*sv.MouseMove += sv_MouseMove;
                sv.Controls["naziv"].MouseMove += sv_MouseMove;
                sv.Controls["bojaEtiketa"].MouseMove += sv_MouseMove;
                sv.Controls["slikaEtiketa"].MouseMove += sv_MouseMove;*/

                panel9.Controls.Add(sv);
            }
 
    }

    private void panel8_DragEnter(object sender, DragEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("drag enter...");
    }

    private void panel8_DragLeave(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("drag leave...");
    }

    private void panel8_DragDrop(object sender, DragEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("drag drop...");
    }

    private void panel8_MouseDown(object sender, MouseEventArgs e)
    {
        if(dozvoljenDragDrop)
            panel8.DoDragDrop(panel8, DragDropEffects.Copy);
    }

    private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
    {
        if (dozvoljenDragDrop)
            panel8.DoDragDrop(panel8, DragDropEffects.Copy);

    }

    private void okvirBoja_MouseDown(object sender, MouseEventArgs e)
    {
        if (dozvoljenDragDrop)
            panel8.DoDragDrop(panel8, DragDropEffects.Copy);

    }

    private void etiketaSlika_MouseDown(object sender, MouseEventArgs e)
    {
        if (dozvoljenDragDrop)
            panel8.DoDragDrop(panel8, DragDropEffects.Copy);

    }

    private void etiketaLabela_MouseDown(object sender, MouseEventArgs e)
    {
        if (dozvoljenDragDrop)
            panel8.DoDragDrop(panel8, DragDropEffects.Copy);

    }

    private void panel9_DragEnter(object sender, DragEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("PANEL 9 DRAG ENTER!!");
        
        Type tip = new StikerView().GetType();
        dragEnterPanel9 = true;
        eDragDrop = e;

        if (e.Data.GetType() == tip)
            e.Effect = DragDropEffects.None;
        else
            e.Effect = DragDropEffects.Copy;
    }

    private void panel9_DragDrop(object sender, DragEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("panel9 drag drop...");

        Vrsta vrsta = model.vrste[selectedId - 1];
        StikerView sv = new StikerView(vrsta);
        sv.Location = panel9.PointToClient(new Point(e.X-37, e.Y));        // globalne koordinate u lokalne koordinate..

        sv.MouseDown += sv_MouseDown;
        sv.Controls["naziv"].MouseDown += sv_MouseDown;
        sv.Controls["bojaEtiketa"].MouseDown += sv_MouseDown;
        sv.Controls["slikaEtiketa"].MouseDown += sv_MouseDown;


        sv.MouseUp += sv_MouseUp;
        sv.Controls["naziv"].MouseUp += sv_MouseUp;
        sv.Controls["bojaEtiketa"].MouseUp += sv_MouseUp;
        sv.Controls["slikaEtiketa"].MouseUp += sv_MouseUp;

       /* sv.MouseMove += sv_MouseMove;
        sv.Controls["naziv"].MouseMove += sv_MouseMove;
        sv.Controls["bojaEtiketa"].MouseMove += sv_MouseMove;
        sv.Controls["slikaEtiketa"].MouseMove += sv_MouseMove;*/

        vrsta.aktivnaEtiketa.x = sv.Location.X;
        vrsta.aktivnaEtiketa.y = sv.Location.Y;

        panel9.Controls.Add(sv);        // add na panel

        model.stikeriMapa.Add(vrsta);                                          // snimanje u model

    }

    void sv_MouseUp(object sender, MouseEventArgs e)
    {

        /*System.Diagnostics.Debug.WriteLine("AKTIVAN STIKER-oznaka: " + aktivanStikerView.vrsta.aktivnaEtiketa.oznaka);
        System.Diagnostics.Debug.WriteLine("AKTIVAN STIKER-broj etiketa: " + aktivanStikerView.vrsta.etikete.Count);
        System.Diagnostics.Debug.WriteLine("AKTIVAN STIKER-pozicija: " + aktivanStikerView.vrsta.aktivnaEtiketa.x + " ," + aktivanStikerView.vrsta.aktivnaEtiketa.y);*/
        
        selektovan = false;
        panel9.Controls["pictureBox1"].Location = new Point(aktivanStikerView.Location.X - 8, aktivanStikerView.Location.Y - 10);


        switch (e.Button)
        {
            case MouseButtons.Right:
                {
                    stikerContextMenu.Show( aktivanStikerView.PointToScreen(new Point(e.X, e.Y)));
                }
                break;
        }

        if (nadKantom)
        {
            int idxStiker = model.stikeriMapa.FindIndex(item => item.aktivnaEtiketa.oznaka == aktivanStikerView.vrsta.aktivnaEtiketa.oznaka);
            model.stikeriMapa.RemoveAt(idxStiker);
            aktivanStikerView.Hide();
            /*panel9.Controls["panel7"].BackColor = Color.White;
            panel9.Controls["panel7"].BackgroundImage = AleksandarBosnjak.Properties.Resources.kanta;
            panel9.Controls["panel7"].Refresh();*/
            panel9.Controls["pictureBox1"].Visible = false;
            nadKantom = false;
        }
    }

    void sv_MouseMove(object sender, MouseEventArgs e)
    {
        Control c = (Control)sender;
        System.Diagnostics.Debug.WriteLine("GLOBALNE KOORDINATE KONTROLE: " + c.PointToScreen(new Point(e.X, e.Y)));
        System.Diagnostics.Debug.WriteLine("RODITELJ JE: " + c.Parent);
        //return;
        StikerView sv = new StikerView();
        if (c.Parent is AleksandarBosnjak.StikerView)
        {
            sv = (StikerView)c.Parent;
        }
        else if (c.Parent is System.Windows.Forms.Panel)
        {
            sv = (StikerView)sender;
        }

        aktivanStikerView = sv;
        if (e.Button == MouseButtons.Left)
        {
            if (selektovan == true)
            {
                sv.Location = panel9.PointToClient(e.Location);
                sv.vrsta.aktivnaEtiketa.x = sv.Location.X;
                sv.vrsta.aktivnaEtiketa.y = sv.Location.Y;

                Rectangle gloXY = sv.RectangleToScreen(sv.Bounds);

                /*if (panel9.Controls["panel7"].Bounds.IntersectsWith(sv.Bounds))         // nad kantom je
                {
                    panel9.Controls["panel7"].BackColor = Color.LightGreen;
                    panel9.Controls["panel7"].Refresh();
                    nadKantom = true;
                }
                else
                {
                    panel9.Controls["panel7"].BackColor = Color.White;
                    panel9.Controls["panel7"].Refresh();
                    nadKantom = false;
                }*/

                sv.ContextMenuStrip = stikerContextMenu;

            }
        }
    }
    
    void sv_MouseDown(object sender, MouseEventArgs e)
    {

        StikerView sv = new StikerView();
        Control c = (Control)sender;
        int x;

        if (c.Parent is AleksandarBosnjak.StikerView)
        {
            sv = (StikerView)c.Parent;
        }
        else if (c.Parent is System.Windows.Forms.Panel)
        {
            sv = (StikerView)sender;
        }

        selektovan = true;
        aktivanStikerView = sv;


        for (int i = 0; i < dataGridView1.Rows.Count; i++) {
            if (dataGridView1.Rows[i].Cells["Oznaka"].Value == aktivanStikerView.vrsta.oznaka) {
                dataGridView1.ClearSelection();
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[i].Index;
                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.Refresh();
            }
        }

            if (panel9.Controls["pictureBox1"] != null)
            {
                panel9.Controls.SetChildIndex(panel9.Controls["pictureBox1"], panel9.Controls.Count);
                panel9.Controls["pictureBox1"].Location = new Point(aktivanStikerView.Location.X - 8, aktivanStikerView.Location.Y-10);
                panel9.Controls["pictureBox1"].Visible = true;
            }


    }


    private void panel7_DragEnter(object sender, DragEventArgs e)
    {

       /* if (sender is StikerView)
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;

       System.Diagnostics.Debug.WriteLine("korpa za otpake enter!");*/
    }

    private void panel7_DragDrop(object sender, DragEventArgs e)
    {
       /* StikerView sv = (StikerView)sender;
        Vrsta vr = sv.vrsta;
        int idxStiker = model.stikeriMapa.FindIndex(item => item.aktivnaEtiketa.oznaka == sv.vrsta.aktivnaEtiketa.oznaka);

        model.stikeriMapa.RemoveAt(idxStiker);*/
    }

    private void panel9_MouseClick(object sender, MouseEventArgs e)
    {
        selektovan = false;
        panel9.Controls["pictureBox1"].Visible = false;
    }


    /*protected override void OnDragDrop(DragEventArgs e)
    {
        pictureBox2.Location = this.PointToClient(new Point(e.X - pictureBox2.Width / 2, e.Y - pictureBox2.Height / 2));
    }*/



    public bool nadKantom { get; set; }

    private void button11_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(Directory.GetCurrentDirectory());
        Help.ShowHelp(this, "../../help/PomocDatoteka.chm");
    }

    public bool refreshed { get; set; }

    private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        String nazivKolone = dataGridView1.Columns[e.ColumnIndex].HeaderText;
        String nazivVrste = (dataGridView1.Rows[e.RowIndex].Cells["Naziv"].Value).ToString();
        String novaVrednost = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ToString();

        int idx = model.vrste.FindIndex(item => item.naziv == nazivVrste);

        if (nazivKolone.Equals("Opis"))
        {
            model.vrste[idx].opis = novaVrednost;
        }
        else if (nazivKolone.Equals("Oznaka"))
        {
            model.vrste[idx].oznaka = novaVrednost;
        }
        else if (nazivKolone.Equals("Prihodi ($)"))
        {
            model.vrste[idx].godisnjiPrihod = int.Parse(novaVrednost);
        }
        else if (nazivKolone.Equals("Status ugroženosti"))
        {
            model.vrste[idx].statusUgrozenosti = novaVrednost;
        }
        else if (nazivKolone.Equals("Turistički status"))
        {
            model.vrste[idx].turistickiStatus = novaVrednost;
        }
        else if (nazivKolone.Equals("Urbana sredina")) {
            if (novaVrednost.Equals("DA"))
                model.vrste[idx].urbanaSredina = true;
            else
                model.vrste[idx].urbanaSredina = false;
        }
        else if (nazivKolone.Equals("Opasna po ljude"))
        {
            if (novaVrednost.Equals("DA"))
                model.vrste[idx].opasnaZaLjude = true;
            else
                model.vrste[idx].opasnaZaLjude = false;
        }
        else if (nazivKolone.Equals("IUCN lista"))
        {
            if (novaVrednost.Equals("DA"))
                model.vrste[idx].iucnLista = true;
            else
                model.vrste[idx].iucnLista = false;
        }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBox1.SelectedIndex != -1)
        {
            textBox1.Enabled = true;
            filterKolona = comboBox1.Text;
        }
        else {
            textBox1.Enabled = false;
            filterKolona = "";
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        Main_Activated(new object(), null);
    }

    private void levoDugme_Click(object sender, EventArgs e)
    {
        int etCount = model.vrste[selectedId-1].etiketaCount;
        model.vrste[selectedId - 1].etiketaCount--;

        foreach (Vrsta vr in model.stikeriMapa) {
            if (vr.aktivnaEtiketa.oznaka.Equals(aktivanStikerView.vrsta.aktivnaEtiketa.oznaka)) {
                aktivanStikerView.vrsta.aktivnaEtiketa.x = vr.aktivnaEtiketa.x;
                aktivanStikerView.vrsta.aktivnaEtiketa.y = vr.aktivnaEtiketa.y;
            }
        }



        System.Diagnostics.Debug.WriteLine("AKTIVAN STIKER-oznaka: " + aktivanStikerView.vrsta.aktivnaEtiketa.oznaka);
        System.Diagnostics.Debug.WriteLine("AKTIVAN STIKER-broj etiketa: " + aktivanStikerView.vrsta.etikete.Count);
        System.Diagnostics.Debug.WriteLine("AKTIVAN STIKER-pozicija: " + aktivanStikerView.vrsta.aktivnaEtiketa.x + " ," + aktivanStikerView.vrsta.aktivnaEtiketa.y);



        model.vrste[selectedId - 1].aktivnaEtiketa = model.vrste[selectedId - 1].etikete[
             model.vrste[selectedId - 1].etiketaCount];

        Main_Activated(new object(), null);
       
    }

    private void desnoDugme_Click(object sender, EventArgs e)
    {
        int etCount = model.vrste[selectedId - 1].etiketaCount;

        model.vrste[selectedId - 1].etiketaCount++;

        foreach (Vrsta vr in model.stikeriMapa)
        {
            if (vr.aktivnaEtiketa.oznaka.Equals(aktivanStikerView.vrsta.aktivnaEtiketa.oznaka))
            {
                vr.aktivnaEtiketa.x = aktivanStikerView.vrsta.aktivnaEtiketa.x;
                vr.aktivnaEtiketa.y = aktivanStikerView.vrsta.aktivnaEtiketa.y;
            }
        }

        if (model.vrste[selectedId - 1].etiketaCount >= model.vrste[selectedId - 1].etikete.Count) {
            desnoDugme.Enabled = false;
            return;
        }

        model.vrste[selectedId - 1].aktivnaEtiketa = model.vrste[selectedId - 1].etikete[
             model.vrste[selectedId - 1].etiketaCount];

        Main_Activated(new object(), null);

    }

    private void panel9_DragOver(object sender, DragEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("e: " + panel9.PointToClient(new Point(e.X, e.Y)));
       
        e.Effect = DragDropEffects.Copy;
        
        if (dragEnterPanel9)
        {

            foreach (Control c in panel9.Controls)
            {
                System.Diagnostics.Debug.WriteLine("st:" + c.Location);

                if (c.Bounds.Contains(panel9.PointToClient(new Point(e.X, e.Y))))
                {
                    e.Effect = DragDropEffects.None;
                }
            }

        }
    }

    }
}
