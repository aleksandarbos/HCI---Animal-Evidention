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
    public partial class DodavanjeEtiketa : Form
    {

        private String izabranaVrsta;
        private String izabranaOznakaEtikete;
        public static String MODE = "UNOS";
        private Model model;
        private Boolean b1, b2, b3 = false;
        private Etiketa aktivnaEtiketa = null;

        public DodavanjeEtiketa(Model model)
        {
            InitializeComponent();
            this.model = model;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            panel2.BackColor = colorDialog1.Color;
            pictureBox6.Image = AleksandarBosnjak.Properties.Resources._3_g;

            b3 = true;

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            //Vrsta izabrano = (Vrsta) (comboBox1.SelectedItem);
            izabranaVrsta = model.vrste[Main.selectedId - 1].naziv;
            
            Etiketa et = new Etiketa(textBox1.Text, colorDialog1.Color.ToArgb(), textBox2.Text);
            
            for (i = 0; i < model.vrste.Count; i++) {
                if (model.vrste[i].naziv.Equals(izabranaVrsta))
                    break;
            }

            if (MODE.Equals("UNOS"))
            {
                model.vrste[i].etikete.Add(et);                     // dodavanje nove
            }
            else {
                et.boja = panel2.BackColor.ToArgb();
                for (int k = 0; k < model.vrste.Count; k++) {
                    for (int m = 0; m < model.vrste[k].etikete.Count; m++) {        // izmena u modelu!
                        if (model.vrste[k].etikete[m].oznaka.Equals(izabranaOznakaEtikete))
                        {
                            model.vrste[k].etikete[m] = et;
                            model.vrste[k].aktivnaEtiketa = et;
                            break;
                        }
                    }
                }

            }
            this.Close();
        }

        private void DodavanjeEtiketa_Activated(object sender, EventArgs e)
        {
            
            //comboBox1.Items.Clear();        // reset

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //groupBox1.Enabled = true;
            //System.Diagnostics.Debug.WriteLine("izabrano: " + comboBox1.SelectedItem);
           // izabrano = (String)(comboBox1.SelectedItem);
        }

        public void initData(String headingLabel, String izabranaVrsta, String oznaka, String opis, int boja,String dugmeNatpis, Etiketa aktivnaEtiketa)
        {
            label1.Text = headingLabel;
            this.izabranaVrsta = izabranaVrsta;
            textBox1.Text = oznaka;
            this.izabranaOznakaEtikete = oznaka;
            textBox2.Text = opis;
            this.aktivnaEtiketa = aktivnaEtiketa;
            //int bojaa = boja;
            panel2.BackColor = Color.FromArgb(boja);
            button2.Text = dugmeNatpis;
            pictureBox1.Image = global::AleksandarBosnjak.Properties.Resources.izmeniSticker;
            b1 = b2 = b3 = true;
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
            else {
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
            return b1 && b2 && b3;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            String str = textBox2.Text;

            if (str == "")
            {
                textBox2.BackColor = Color.White;
                pictureBox4.Image = global::AleksandarBosnjak.Properties.Resources._2;
                b2 = false;
            }
            else
            {
                textBox2.BackColor = Color.LightGreen;
                pictureBox4.Image = global::AleksandarBosnjak.Properties.Resources._2_g;
                b2 = true;
            }

            if (sviOdradjeni())
                button2.Enabled = true;
            else
                button2.Enabled = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "../../help/PomocDatoteka.chm", HelpNavigator.KeywordIndex, "etiketa");

        }
    }
}
