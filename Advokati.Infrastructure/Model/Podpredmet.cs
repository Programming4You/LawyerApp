using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Advokati.Infrastructure.Model
{
    public class Podpredmet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unesite naziv podpredmeta!")]
        [DisplayName("Naziv podpredmeta")]
        [MaxLength(50)]
        public string NazivPodpredmeta { get; set; }

        [Required(ErrorMessage = "Unesite šifru podpredmeta!")]
        [DisplayName("Šifra")]
        [MaxLength(50)]
        public string Sifra { get; set; }

        [ForeignKey("Predmet")]
        public int PredmetId { get; set; }

        public Predmet Predmet { get; set; }
    }
}
