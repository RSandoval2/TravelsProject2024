using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TravelsProject2024.EN
{
    public class TripScheduling
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TouristPlaces")]
        [Required(ErrorMessage = "La referencia del anuncio es requerida")]
        [Display(Name = "Anuncio")]
        public int IdTouristPlaces { get; set; }

        [ForeignKey("Vehicles")]
        [Required(ErrorMessage = "La referencia del vehiculo es requerida")]
        [Display(Name = "Anuncio")]
        public int vehicles { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Fecha del Registro")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Precio ")]
        public Decimal Price { get; set; }


        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Estado ")]
        public Byte status { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }


    }
}
