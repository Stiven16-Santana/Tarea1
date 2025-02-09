using System.ComponentModel.DataAnnotations;

namespace Tarea1.Models
{
    public class Sistemas
    {
        [Key]

        [Required(ErrorMessage = "Este campo es requerido.")]
        public int SistemaId { get; set; }
       

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Descripcion { get; set; }
        

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Complejidad { get; set; }
        

    } 
}
