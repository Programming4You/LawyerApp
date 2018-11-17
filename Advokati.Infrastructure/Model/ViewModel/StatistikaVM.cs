using System.ComponentModel;


namespace Advokati.Infrastructure.Model.ViewModel
{
    public class StatistikaVM
    {
        public int Id { get; set; }
        public string Ime { get; set; }

        public string Prezime { get; set; }

        [DisplayName("Broj taskova")]
        public int BrojTaskova { get; set; }

        [DisplayName("Broj klijenata")]
        public int BrojKlijenata { get; set; }

        [DisplayName("Broj podpredmeta")]
        public int BrojPodpredmeta { get; set; }

        [DisplayName("Ukupno vreme (h)")]
        public int UkupnoVreme { get; set; }

        [DisplayName("Ukupna zarada (EUR)")]
        public int Zarada { get; set; }

    }
}
