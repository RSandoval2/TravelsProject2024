using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelsProject2024.EN
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de vehiculo es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "Tipo")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "La capacidad maxima es requerida")]
        [Display(Name = "Capacidad")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "La marca del vehiculo es obligatoria")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "Marca")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo del vehiculo es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "Modelo")]
        public  string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año es requerido")]
        [Display(Name = "Año")]
        public int Year { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }//Propiedad auxiliar
        
    }
}
