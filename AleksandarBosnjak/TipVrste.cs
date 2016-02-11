using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AleksandarBosnjak
{
    [Serializable()]
    public class TipVrste
    {
        public String ime { get; set;}
        public String opis { get; set; }
        public Image slicica { get; set; }
        public String oznaka { get; set; }

        public TipVrste(String ime, Image slicica, String opis, String oznaka)
        {
            this.ime = ime;
            this.opis = opis;
            this.slicica = slicica;
            this.oznaka = oznaka;
        }

        public TipVrste () {}
    }
}
