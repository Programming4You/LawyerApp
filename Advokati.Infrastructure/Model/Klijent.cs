using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Advokati.Infrastructure.Model
{
    public class Klijent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unesite naziv klijenta!")]
        [MaxLength(50, ErrorMessage = "Naziv klijenta može imati maksimum 50 karaktera!")]
        [DisplayName("Naziv klijenta")]
        public string Naziv { get; set; }

        [ForeignKey("Advokat")]
        public int AdvokatId { get; set; }

        public Advokat Advokat { get; set; }
    }
}
