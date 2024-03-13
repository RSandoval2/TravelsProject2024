using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelsProject2024.EN
{
    public class TouristPlaceImage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TouristPlaces")]

        [Required(ErrorMessage = "La referencia del anuncio es requerida")]
        [Display(Name = "Anuncio")]
        public int IdTouristPlaces { get; set; }

        [Required(ErrorMessage = "La ruta del archivo es requerida")]
        [Display(Name = "Ruta")]
        public string Path { get; set; } = string.Empty;

        [NotMapped]
        public int Top_Aux { get; set; } //propiedad auxiliar
        public TouristPlaces TouristPlaces { get; set; } = new TouristPlaces(); //propiedad de navegacion
    }
}
