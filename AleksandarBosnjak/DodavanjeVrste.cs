using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace AleksandarBosnjak
{
    public partial class DodavanjeVrste : Form
    {

        private Boolean b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11 = false;
        private Boolean opasnaZaLjude = false;
        private Boolean urbanaSredina = false;
        private Boolean iucnLista = false;
        public static String MODE = "UNOS";
        private int izabranaVrsta;
        private OpenFileDialog open = new OpenFileDialog();
        private Image selectedImage { get; set; }
        private Model model;
        private String stariNaziv = "";
        private Etiketa aktivnaEtiketa = null;

        public DodavanjeVrste(Model model)
        {
            InitializeComponent();
            this.model = model;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String str = textBox1.Text;

            if (!str.All(char.IsDigit))
            {
                textBox1.BackColor = Color.LightGreen;
                b1 = true;
                pictureBox6.Image = global::AleksandarBosnjak.Properties.Resources._1_g;
            }
            else
            {
                textBox1.BackColor = Color.IndianRed;
                pictureBox6.Image = global::AleksandarBosnjak.Properties.Resources._1;
                b1 = false;
            }
            if (str == "")
            {
                textBox1.BackColor = Color.White;
                pictureBox6.Image = global::AleksandarBosnjak.Properties.Resources._1;
                b1 = false;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }



        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            String str = textBox3.Text;

            if (str.All(char.IsDigit))
            {
                textBox3.BackColor = Color.LightGreen;
                b3 = true;
                pictureBox8.Image = global::AleksandarBosnjak.Properties.Resources._3_g;
            }
            else
            {
                textBox3.BackColor = Color.IndianRed;
                pictureBox8.Image = global::AleksandarBosnjak.Properties.Resources._3;
                b3 = false;
            }
            if (str == "")
            {
                textBox3.BackColor = Color.White;
                pictureBox8.Image = global::AleksandarBosnjak.Properties.Resources._3;
                b3 = false;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false; 
            
            /*b3 = true;
             pictureBox8.Image = global::AleksandarBosnjak.Properties.Resources._3_g;*/
        }

        // ------
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox9.Image = global::AleksandarBosnjak.Properties.Resources._4_g;
            opasnaZaLjude = true;
            b4 = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox9.Image = global::AleksandarBosnjak.Properties.Resources._4_g;
            opasnaZaLjude = false;
            b4 = true;

        }
        // -------

        // -------
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox10.Image = global::AleksandarBosnjak.Properties.Resources._5_g;
            iucnLista = true;
            b5 = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox10.Image = global::AleksandarBosnjak.Properties.Resources._5_g;
            iucnLista = false;
            b5 = true;
        }
        // -------

        // -------
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox11.Image = global::AleksandarBosnjak.Properties.Resources._6_g;
            urbanaSredina = true;
            b6 = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox11.Image = global::AleksandarBosnjak.Properties.Resources._6_g;
            urbanaSredina = false;
            b6 = true;
        }
        // ---------

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex != -1)
            {
                comboBox1.BackColor = Color.LightGreen;
                b7 = true;
                pictureBox12.Image = global::AleksandarBosnjak.Properties.Resources._7_g;
            }
            else
            {
                comboBox1.BackColor = Color.White;
                b7 = false;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)     // etikete izmena
        {

            if (comboBox2.SelectedIndex != -1)
            {
                comboBox2.BackColor = Color.LightGreen;
                pictureBox13.Image = global::AleksandarBosnjak.Properties.Resources._8_g;
                //b7 = true;

                int idxAktivnaEtiketa;
                for (int i = 0; i < model.vrste.Count; i++) {
                    if (model.vrste[i].Equals(textBox1.Text)) {
                        idxAktivnaEtiketa = model.vrste[i].postaviAktivnuEtiketu(comboBox2.SelectedText);
                        break;
                    }
                }

            }
            else
            {
                comboBox2.BackColor = Color.White;
                //b7 = false;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false;

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox3.SelectedIndex != -1)
            {
                comboBox3.BackColor = Color.LightGreen;
                b8 = true;
                pictureBox14.Image = global::AleksandarBosnjak.Properties.Resources._9_g;
            }
            else
            {
                comboBox3.BackColor = Color.White;
                b8 = false;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox15.Image = global::AleksandarBosnjak.Properties.Resources._10_g;
            b9 = true;

        }


        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)     // combo tip vrste
        {
            izabranaVrsta = comboBox4.SelectedIndex;

            if (comboBox4.SelectedIndex != -1)
            {
                comboBox4.BackColor = Color.LightGreen;
                b2 = true;
                pictureBox7.Image = global::AleksandarBosnjak.Properties.Resources._2_g;

                System.Diagnostics.Debug.WriteLine("INDEX COMBOBOx4: " +  comboBox4.SelectedIndex);

                comboBox2.Items.Clear();        // ponistavanje
                for (int i = 0; i < model.vrste.Count; i++) {                   // izlistavenje etiketa u dodaj vrstu
                    if (model.vrste[i].tipVrste.ime.Equals(comboBox4.Text)) {
                        for (int j = 0; j < model.vrste[i].etikete.Count; j++) {
                            comboBox2.Items.Add(model.vrste[i].etikete[j].oznaka);
                        }
                    }
                    break;
                }

            }
            else
            {
                comboBox4.BackColor = Color.White;
                pictureBox7.Image = AleksandarBosnjak.Properties.Resources._2;
                b2 = false;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private Boolean sviOdradjeni()
        {
            return b1 && b2 && b3 &&
                   b4 && b5 && b6 &&
                   b7 && b8 && b9 &&
                   b10 && b11;
            //return b1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           string oznaka = textBox4.Text;
           string naziv = textBox1.Text;
           String opis = textBox5.Text;
           int prihodi = int.Parse(textBox3.Text); 
           //int dx = comboBox4.SelectedIndex;
           TipVrste tipVrste = model.tipoviVrste[izabranaVrsta];
           Etiketa et = new Etiketa();
           int statusUgrozenosti = comboBox1.SelectedIndex;
           int turistickiStatus = comboBox3.SelectedIndex;
           DateTime datum = dateTimePicker1.Value;
           String etiketa = comboBox2.Text;

           foreach(TipVrste tv in model.tipoviVrste) {
               if (comboBox4.Text.Equals(tv.ime)) {
                   tipVrste = tv;
               }
           }
           
           for(int i = 0; i < model.vrste.Count; i++) {
                for(int j = 0; j < model.vrste[i].etikete.Count; j++) {
                    if(model.vrste[i].etikete[j].oznaka.Equals(etiketa)){
                        et = model.vrste[i].etikete[j];
                        break;
                    }
                }
           }
           Etiketa staraEtiketa = new Etiketa();

           Vrsta vrsta = new Vrsta();
         
           if (MODE.Equals("UNOS"))
           {                             // novi unos
               et.oznaka = "";

               vrsta = new Vrsta(oznaka, naziv, opis, tipVrste, statusUgrozenosti, turistickiStatus, urbanaSredina,
                                        opasnaZaLjude, iucnLista, prihodi, datum, et, pictureBox5.Image);

               model.vrste.Add(vrsta);
           }
           else
           {
               vrsta = new Vrsta(oznaka, naziv, opis, tipVrste, statusUgrozenosti, turistickiStatus, urbanaSredina,
                                        opasnaZaLjude, iucnLista, prihodi, datum, et, pictureBox5.Image);

               // izmena postojeceg unosa
               pictureBox7.Image = AleksandarBosnjak.Properties.Resources._2_g;
               for (int i = 0; i < model.vrste.Count; i++)
               {
                   if (model.vrste[i].naziv.Equals(stariNaziv))
                   {
                       //vrsta.etikete = model.vrste[i].etikete;
                       model.vrste[i].aktivnaEtiketa = et;
                       model.vrste[i].datumOtkrivanja = datum;
                       model.vrste[i].etiketaBoja = et.boja;
                       model.vrste[i].godisnjiPrihod = prihodi;
                       model.vrste[i].iucnLista = iucnLista;
                       model.vrste[i].naziv = textBox1.Text;
                       model.vrste[i].opis = opis;
                       model.vrste[i].oznaka = oznaka;
                       break;
                   }
               }
           }
           
            // zatvori prozor

           this.Close();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void DodavanjeVrste_Activated(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();        // resetovanje liste

            for (int i = 0; i < model.tipoviVrste.Count; i++) {
                comboBox4.Items.Add(model.tipoviVrste[i].ime);
                b2 = true;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false;



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        public void initData(String headingLabel, String picName, String nazivVrste, String tipVrste,
                            int godisnjiPrihodi, Boolean opasnaZaLjude,Boolean iucnLista, Boolean urbanaVrsta,
                            String statusUgrozenosti, String turistickiStatus, DateTime datum,
                            String oznakaVrste, String opisVrste, Image slicica, String dugmeNatpis, Etiketa aktivnaEtiketa) {
            
            int idx = 0;

           // nazivGlobal = nazivVrste;           // smestanje u global, za init etiketa combobox

            label1.Text = headingLabel;
            pictureBox1.Image = global::AleksandarBosnjak.Properties.Resources.izmeniSticker;
            textBox1.Text = nazivVrste;
            stariNaziv = (String)textBox1.Text.Clone();

            this.aktivnaEtiketa = aktivnaEtiketa;

            comboBox4.Text = tipVrste;
            comboBox4.BackColor = Color.LightGreen;

            textBox3.Text = godisnjiPrihodi.ToString();
            
            if(opasnaZaLjude) {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            } else {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }

            if (iucnLista)
            {
                radioButton4.Checked = true;
                radioButton3.Checked = false;
            }
            else
            {
                radioButton3.Checked = true;
                radioButton4.Checked = false;
            }

            if (urbanaVrsta)
            {
                radioButton6.Checked = true;
                radioButton5.Checked = false;
            }
            else
            {
                radioButton5.Checked = true;
                radioButton6.Checked = false;
            }

            comboBox1.Text = statusUgrozenosti;
            comboBox3.Text = turistickiStatus;
            
            dateTimePicker1.Value = datum;
            
            textBox4.Text = oznakaVrste;
            textBox5.Text = opisVrste;


            button2.Text = dugmeNatpis;

            pictureBox5.Image = slicica;

            comboBox2.Items.Clear();
            for (int i = 0; i < model.vrste.Count; i++)
            {
                if (model.vrste[i].naziv.Equals(nazivVrste))
                {
                    for (int j = 0; j < model.vrste[i].etikete.Count; j++)
                    {
                        comboBox2.Items.Add(model.vrste[i].etikete[j].oznaka);
                        //return;
                    }
                    comboBox2.Text = model.vrste[i].aktivnaEtiketa.oznaka;
                }
            }

            pictureBox7.Image = AleksandarBosnjak.Properties.Resources._2_g;
            
        }



        public void sacuvaj()
        {
            button2_Click(new object(), new EventArgs());       // pokreni click
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            open.Filter = "Slike (*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(open.FileName);
                pictureBox5.Image = img;
                selectedImage = img;
                panel2.BackColor = Color.LightGreen;
                //pictureBox5.ImageLocation = open.SafeFileName;
                //b6 = true;
                pictureBox4.Image = global::AleksandarBosnjak.Properties.Resources._13_g;

                /*if (sviOdradjeni())
                    button2.Enabled = true;
                else
                    button2.Enabled = false;*/
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            String str = comboBox4.Text;
            if (str.Equals(""))
            {
                textBox4.BackColor = Color.White;
                pictureBox17.Image = global::AleksandarBosnjak.Properties.Resources._11;
                b10 = false;
            }
            else {
                textBox4.BackColor = Color.LightGreen;
                pictureBox17.Image = global::AleksandarBosnjak.Properties.Resources._11_g;
                b10 = true;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false; 

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            String str = comboBox4.Text;
            if (str.Equals(""))
            {
                textBox5.BackColor = Color.White;
                pictureBox18.Image = AleksandarBosnjak.Properties.Resources._12;
                b11 = false;
            }
            else
            {
                textBox5.BackColor = Color.LightGreen;
                pictureBox18.Image = AleksandarBosnjak.Properties.Resources._12_g;
                b11 = true;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false; 

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "../../help/PomocDatoteka.chm", HelpNavigator.KeywordIndex, "vrste");
        }
    }
}
