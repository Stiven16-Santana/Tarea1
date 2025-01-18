using System.ComponentModel.DataAnnotations;

namespace Tarea1.Models;

public class Tecnicos
{
    [Key]

    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]

    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "El sueldo por hora es requerido.")]
    [Range(0, 2000000000, ErrorMessage = "El sueldo debe ser un valor positivo.")]
    public decimal SueldoHora {get; set;}
}
