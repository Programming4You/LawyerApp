using System.ComponentModel.DataAnnotations;


namespace Advokati.Infrastructure.Model
{
    public class Advokat
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje ime je obavezno!")]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Polje prezime je obavezno!")]
        [MaxLength(50)]
        public string Prezime { get; set; }


        [Required(ErrorMessage = "Slika advokata je obavezna!")]
        public string AdvokatImage { get; set; }
    }
}
