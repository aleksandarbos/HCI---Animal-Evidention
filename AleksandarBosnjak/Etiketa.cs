using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AleksandarBosnjak
{
    [Serializable()]
    public class Etiketa
    {
        public String oznaka { get; set; }
        public int boja { get; set; }
        public String opis { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public bool nova { get; set; }

        public Etiketa(String oznaka, int boja, String opis) {
            this.oznaka = oznaka;
            this.boja = boja;
            this.opis = opis;
            this.x = 0;
            this.y = 0;
            this.nova = true;
        }
        public Etiketa() { }
    }
}
