using System;
using System.ComponentModel;


namespace Advokati.Infrastructure.Model.ViewModel
{
    public class AdvokatKlijentVM
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }

        [DisplayName("Naziv klijenta")]
        public string Naziv { get; set; }

        [DisplayName("Utrošeno vreme")]
        public int UtrosenoVreme { get; set; }

        [DisplayName("Cena u EUR")]
        public int Cena
        {
            get { return this.UtrosenoVreme * 50; }
        }

        [DisplayName("Plaćeno")]
        public bool Placeno { get; set; }

        public string Opis { get; set; }

        [DisplayName("Naziv podpredmeta")]
        public string Podpredmet { get; set; }
    }
}
