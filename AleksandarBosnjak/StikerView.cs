using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AleksandarBosnjak
{
    public class StikerView : Panel
    {
        public Vrsta vrsta { get; set; }

        public const int dimensionSize = 75;

        private Size size;
        public Label naziv = new Label();
        public PictureBox bojaEtiketa = new PictureBox();
        public PictureBox slikaEtiketa = new PictureBox();

        public StikerView(Vrsta vrsta)
        {
            this.vrsta = vrsta;
            this.BackgroundImage = global::AleksandarBosnjak.Properties.Resources.stiker1;
            this.bojaEtiketa.BackColor = Color.FromArgb(vrsta.aktivnaEtiketa.boja);      // FromARgb(etiketa.boja);
            this.naziv.Text = vrsta.aktivnaEtiketa.oznaka;     // etiketa.oznaka

            if (vrsta.slicica != null)
            {
                this.slikaEtiketa.Image = this.vrsta.slicica;               // ima sliku vrste, ako ne, sliku tipa vrste..
            }
            else
            {
                this.slikaEtiketa.Image = this.vrsta.tipVrste.slicica;      // ima sliku tipa vrste, ako nema sliku vrste..
            }


            initGUI();
        }

        public StikerView() { }

        private void initGUI()
        {

            this.size.Width = dimensionSize;
            this.size.Height = dimensionSize + 5;
            this.Size = size;

            this.Cursor = Cursors.Hand;

            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Transparent;

            bojaEtiketa.Size = new Size(65, 56);
            bojaEtiketa.Location = new Point(6, 14);
            bojaEtiketa.Name = "bojaEtiketa";

            naziv.Location = new Point(4, 63);
            naziv.BackColor = Color.Transparent;
            naziv.AutoSize = true;
            naziv.TextAlign = ContentAlignment.MiddleCenter;
            naziv.Name = "naziv";

            slikaEtiketa.Size = new Size(56, 45);
            slikaEtiketa.Location = new Point(10, 18);
            slikaEtiketa.SizeMode = PictureBoxSizeMode.StretchImage;
            slikaEtiketa.Name = "slikaEtiketa";

            this.Controls.Add(naziv);
            this.Controls.Add(slikaEtiketa);
            this.Controls.Add(bojaEtiketa);

        }

        
    }
}
