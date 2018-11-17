using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Advokati.Infrastructure.Model
{
    public class Task
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Unesite utrošeno vreme u minutima")]
        [DisplayName("Utrošeno vreme")]
        [Range(1, int.MaxValue, ErrorMessage = "Samo pozitivni brojevi dozvoljeni")]
        public int UtrosenoVreme { get; set; }

        [Required(ErrorMessage = "Opis taska je obavezan!")]
        [MaxLength(150)]
        public string Opis { get; set; }

        [ForeignKey("Advokat")]
        public int AdvokatId { get; set; }

        [ForeignKey("Podpredmet")]
        public int PodpredmetId { get; set; }

        public Advokat Advokat { get; set; }

        [DisplayName("Naziv podpredmeta")]
        public Podpredmet Podpredmet { get; set; }


        [DisplayName("Cena u EUR")]
        public int Cena => this.UtrosenoVreme * 50;

        [DisplayName("Plaćeno")]
        public bool Placeno { get; set; }
    }
}
