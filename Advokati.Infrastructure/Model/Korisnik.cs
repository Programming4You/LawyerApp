using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Advokati.Infrastructure.Model
{
    public class Korisnik
    {
        [Key, ForeignKey("Advokat")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obavezno!")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna!")]
        [MaxLength(50)]
        public string Password { get; set; }

        [ForeignKey("Uloga")]
        public int UlogaId { get; set; }

        public Uloga Uloga { get; set; }

        public Advokat Advokat { get; set; }
    }
}
