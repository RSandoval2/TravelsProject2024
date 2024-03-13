using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelsProject2024.EN
{
    public class TouristPlaces
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        [Display(Name = "Descripción")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Ubicación")]
        public string Location { get; set; } = string.Empty;


        // Propiedad auxiliar
        [NotMapped]
        public int Top_Aux { get; set; }


        // Propiedad de navegación
        public List<TouristPlaceImage> TouristPlacesImages { get; set; } = new List<TouristPlaceImage>();
    }
}
