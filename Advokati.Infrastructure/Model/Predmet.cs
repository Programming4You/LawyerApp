using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Advokati.Infrastructure.Model
{
    public class Predmet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unesite naziv predmeta!")]
        [DisplayName("Naziv predmeta")]
        [MaxLength(50)]
        public string NazivPredmeta { get; set; }

        [Required(ErrorMessage = "Unesite šifru predmeta!")]
        [DisplayName("Šifra")]
        [MaxLength(50)]
        public string Sifra { get; set; }

        [ForeignKey("Klijent")]
        public int KlijentId { get; set; }

        public Klijent Klijent { get; set; }
    }
}
