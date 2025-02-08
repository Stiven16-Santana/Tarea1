using System.ComponentModel.DataAnnotations;

namespace Tarea1.Models;

public class Ciudades
{
    [Key]

    public int CiudadId { get; set; }
    [Required(ErrorMessage ="Este campo es requerido.")]

    [StringLength(100,ErrorMessage ="El nombre no puede exceder de los 100 caracteres.")]
    public string Ciudad { get; set; }
}
