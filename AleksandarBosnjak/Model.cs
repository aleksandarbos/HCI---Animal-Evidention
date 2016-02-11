using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;



namespace AleksandarBosnjak
{
    [Serializable()]
    public class Model
    {
        public List<Vrsta> vrste = new List<Vrsta>();
        public List<TipVrste> tipoviVrste = new List<TipVrste>();
        public List<Vrsta> stikeriMapa = new List<Vrsta>();
        
        public Model()
        {
            initTipovi();   // popunjavanje liste
            initVrste();
            initEtikete();
        }

        private void initVrste()
        {
           /* vrste.Add(new Vrsta("SOM", "Som", "Som brkata riba.",
                new TipVrste("Riba", Image.FromFile("../../images/vrste/ribe.jpg"), "Ljigava stvorenja koja plivaju...", "Rb-14"),
                2, 1, true, false, true, 223, new DateTime(2001, 4, 15),"",Image.FromFile("../../images/vrste/som1.jpg")));

            vrste.Add(new Vrsta("KROK", "Krokodil", "Krokodil strasan.",
                new TipVrste("Sisar", Image.FromFile("../../images/vrste/sisari.jpg"), "Sisari stvorenja na kopnu...", "Si-44"),
                3, 2, false, false, true, 500, new DateTime(2011, 4, 15), "", null));

            vrste.Add(new Vrsta("SKAK", "Skakavac", "Skakavac skace.",
               new TipVrste("Insekt", Image.FromFile("../../images/vrste/insekti.jpg"), "Svakojaka stvorenja koja lete...", "In-22"),
               1, 2, false, true, true, 750, new DateTime(2031, 4, 15), "", null));
            
            vrste.Add(new Vrsta("ORAO", "Orao", "Orao leti.",
               new TipVrste("Ptica", Image.FromFile("../../images/vrste/ptice.jpg"), "Perjana stvorenja koja lete...", "Pt-12"),
               2, 1, false, false, true, 950, new DateTime(2021, 4, 15), "", Image.FromFile("../../images/vrste/orao.jpg")));*/
        }

        private void initTipovi()
        {
            System.Diagnostics.Debug.WriteLine("Trenitni direktorjum" + Directory.GetCurrentDirectory());


            tipoviVrste.Add(new TipVrste("Ptica", Image.FromFile("../../images/vrste/ptice.jpg"), "Perjana stvorenja koja lete...", "oznaka"));
            tipoviVrste.Add(new TipVrste("Riba", Image.FromFile("../../images/vrste/ribe.jpg"), "Ljigava stvorenja koja plivaju...", "Rb-14"));
            tipoviVrste.Add(new TipVrste("Mačka", Image.FromFile("../../images/vrste/macke.jpg"), "Mackata stvorenja...", "Ma-24"));
            tipoviVrste.Add(new TipVrste("Insekt", Image.FromFile("../../images/vrste/insekti.jpg"), "Svakojaka stvorenja koja lete...", "In-22"));
            tipoviVrste.Add(new TipVrste("Sisar", Image.FromFile("../../images/vrste/sisari.jpg"), "Sisari stvorenja na kopnu...", "Si-44"));
        }

        private void initEtikete() {
            vrste[0].etikete.Add(new Etiketa("Dunavski Som", Color.ForestGreen.ToArgb(), "Som koji pliva u Dunavu."));
            vrste[0].etikete.Add(new Etiketa("Beli Som", Color.IndianRed.ToArgb(), "Som koji pliva u bistrim vodama."));
            vrste[0].aktivnaEtiketa = vrste[0].etikete[0];      // inicijalno aktivna etiketa Dunavski Som

            vrste[3].etikete.Add(new Etiketa("Beloglavi orao", Color.Gold.ToArgb(), "Naseljava Americke prostore."));
            vrste[3].etikete.Add(new Etiketa("Dvoglavi orao", Color.GreenYellow.ToArgb(), "Naseljava prostore Srbije."));
            vrste[3].aktivnaEtiketa = vrste[3].etikete[1];      // inicijalna ptica Dvoglavi orao
        }



        
    }



}
