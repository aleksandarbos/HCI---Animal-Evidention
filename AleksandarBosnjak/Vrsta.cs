using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AleksandarBosnjak
{
    [Serializable()]
    public class Vrsta
    {
        public  String oznaka { get; set; }
        public  String naziv { get; set; }
        public String opis { get; set; }
        public  TipVrste tipVrste { get; set; }
        public  enum StautsUgrozenosti {KRITICNO_UGROZENA, UGROZENA, RANJIVA, ZAVISNA_OD_STANISTA, BLIZU_RIZIKA, NAJMANJEG_RIZIKA};
        public String[] statusUgrozenostiNiz = { "Kritično ugrožena", "Ugrožena", "Ranjiva", "Zavisna od očuvanja staništa", "Blizu rizika", "Najmanjeg rizika" };
        public String statusUgrozenosti;
        public  enum TuristickiStatus { IZOLOVANA, DELIMICNO_HABUTIRANA, HABUTIRANA };
        public String[] turistickiStatusNiz = { "Izolovana", "Delimično habutirana", "Habutirana" };
        public String turistickiStatus;
        public  Boolean urbanaSredina { get; set; }
        public Boolean opasnaZaLjude { get; set; }
        public Boolean iucnLista { get; set; }
        public  int godisnjiPrihod { get; set; }
        public  DateTime datumOtkrivanja { get; set; }
        public List<Etiketa> etikete = new List<Etiketa>();
        //public String slicicaURL { get; set; }
        //public String etiketaNaziv { get; set; }
        public int etiketaBoja { get; set; }
        public Etiketa aktivnaEtiketa { get; set; }
        public Image slicica { get; set; }
        public int etiketaCount { get; set; }

        public Vrsta(String oznaka, String naziv, String opis, TipVrste tipVrste, int statusUgrozenosti, int turistickiStatus,
            Boolean urbanaSredina, Boolean opasnaZaLjude, Boolean iucnLista, int godisnjiPrihod, DateTime datumOtkrivanja, Etiketa etiketa, Image slicica)
        {
                  // init etiketa

            this.oznaka = oznaka;
            this.naziv = naziv;
            this.opis = opis;
            this.tipVrste = tipVrste;
            this.statusUgrozenosti = statusUgrozenostiNiz[statusUgrozenosti];
            this.turistickiStatus = turistickiStatusNiz[turistickiStatus];
            this.urbanaSredina = urbanaSredina;
            this.iucnLista = iucnLista;
            this.opasnaZaLjude = opasnaZaLjude;
            this.datumOtkrivanja = datumOtkrivanja;
            this.godisnjiPrihod = godisnjiPrihod;
            if (etiketa != null)
                this.aktivnaEtiketa = etiketa;
            else
                this.aktivnaEtiketa = new Etiketa("neinicjialozivana", 45, "neki opis neizinasdajl");

            if (slicica == null)
                slicica = null;         // ako nije postavljena za vrstu koristi od tipa
            else
                this.slicica = slicica;
        }

        public Vrsta() { }

        public int postaviAktivnuEtiketu(String oznakaEtikete) {
            int i;
            for (i = 0; i < etikete.Count; i++) {
                if (etikete[i].oznaka.Equals(oznakaEtikete)) {
                    aktivnaEtiketa = etikete[i];
                    break;
                }
            }
            return i;                                       // vraca index aktivne etikete u listi etiketa
        }

    }
}
