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
    public partial class DodavanjeTipa : Form
    {
        private OpenFileDialog open = new OpenFileDialog();
        public static string MODE = "UNOS";
        private String stariNaziv;
        private Model model;
        private Boolean b1, b2, b3, b4 = false;

        public DodavanjeTipa(Model model)
        {
            InitializeComponent();
            this.model = model;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            TipVrste tipVr = new TipVrste();

            if (open.FileName.Equals(""))
            {
                tipVr = new TipVrste(textBox1.Text, pictureBox5.Image, textBox2.Text, textBox3.Text);
            }
            else {
                tipVr = new TipVrste(textBox1.Text, Image.FromFile(open.FileName), textBox2.Text, textBox3.Text);
            }

            if (MODE.Equals("UNOS"))
            {
                model.tipoviVrste.Add(tipVr);                       // dodavanje novog 
                
                //model.
            }                               
            else {                                                  // izmena postojeceg
                b1 = b2 = b3 = b4 = true;

                Vrsta vrsta = model.vrste[Main.selectedId - 1];
                for (int i = 0; i < model.tipoviVrste.Count; i++) {
                    if (model.tipoviVrste[i].ime.Equals(stariNaziv)) { 

                        vrsta.tipVrste = tipVr;
                        //model.vrste[i].tipVrste = tipVr;
                        model.tipoviVrste[i] = vrsta.tipVrste;
                        pictureBox5.Image = model.tipoviVrste[i].slicica;

                        //break;
                    }
                }
                for (int i = 0; i < model.vrste.Count; i++) {
                    if (model.vrste[i].tipVrste.ime.Equals(stariNaziv))
                        model.vrste[i].tipVrste = vrsta.tipVrste;
                }

            }
            //DodavanjeVrste dodVr = new DodavanjeVrste();
            //dodVr.sacuvaj();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            open.Filter = "Slike (*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(open.FileName);
                pictureBox5.Image = img;
                panel2.BackColor = Color.LightGreen;
                //pictureBox5.ImageLocation = open.SafeFileName;
                b4 = true;
                pictureBox7.Image = global::AleksandarBosnjak.Properties.Resources._4_g;

                if (sviOdradjeni())
                    button2.Enabled = true;
                else
                    button2.Enabled = false;
            }
        }

        public void initData(String headingLabel, String nazivTipaVrste,
                             String opisTipaVrste, String dugmeNatpis, String oznakaTipaVrste)
        {
            label1.Text = headingLabel;
            textBox1.Text = nazivTipaVrste;
            textBox2.Text = opisTipaVrste;
            textBox3.Text = oznakaTipaVrste;
            button2.Text = dugmeNatpis;
            stariNaziv = nazivTipaVrste;
            if (!MODE.Equals("UNOS"))
            {
                pictureBox5.Image = model.vrste[Main.selectedId - 1].tipVrste.slicica;
                button2.Enabled = true;
            }
        }

        private void DodavanjeTipa_Activated(object sender, EventArgs e)
        {
            // osvezavanje unetih podataka u GUI
            int idxTipVrste;
            for (int i = 0; i < model.tipoviVrste.Count; i++) {
                if (model.vrste[Main.selectedId-1].tipVrste.ime.Equals(model.tipoviVrste[i].ime)) {
                    idxTipVrste = i;
                    break;
                }
            }

            //pictureBox5.Image = model.vrste[Main.selectedId - 1].tipVrste.slicica;
            //textBox3.Text = model.vrste[Main.selectedId - 1].tipVrste.oznaka;
            
            //System.Diagnostics.Debug.WriteLine(" osvezeno ime tipa vrste: " + model.vrste[Main.selectedId-1].tipVrste.ime);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String str = textBox1.Text;

            if (str == "")
            {
                textBox1.BackColor = Color.White;
                pictureBox2.Image = global::AleksandarBosnjak.Properties.Resources._1;
                b1 = false;
            }
            else
            {
                textBox1.BackColor = Color.LightGreen;
                pictureBox2.Image = global::AleksandarBosnjak.Properties.Resources._1_g;
                b1 = true;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false; 

        }

        private bool sviOdradjeni()
        {
            return b1 && b2 &&
                   b3 && b4;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            String str = textBox3.Text;

            if (str == "")
            {
                textBox3.BackColor = Color.White;
                pictureBox3.Image = global::AleksandarBosnjak.Properties.Resources._2;
                b2 = false;
            }
            else
            {
                textBox3.BackColor = Color.LightGreen;
                pictureBox3.Image = global::AleksandarBosnjak.Properties.Resources._2_g;
                b2 = true;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false; 


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            String str = textBox2.Text;

            if (str == "")
            {
                textBox2.BackColor = Color.White;
                pictureBox6.Image = global::AleksandarBosnjak.Properties.Resources._3;
                b3 = false;
            }
            else
            {
                textBox2.BackColor = Color.LightGreen;
                pictureBox6.Image = global::AleksandarBosnjak.Properties.Resources._3_g;
                b3 = true;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false; 


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "../../help/PomocDatoteka.chm", HelpNavigator.KeywordIndex, "tipa");
        }

    }
}
