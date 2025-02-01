using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tarea1.Models;

public class Tickets
{
    [Key]
    public int TicketId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido.")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Este campo requerido.")]
    public int Prioridad { get; set; }

    [Required(ErrorMessage = "El ClienteId es requerido.")]
    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    public Clientes? cliente { get; set; }

    [Required(ErrorMessage = "Este campo es requerido.")]
    [StringLength(100, ErrorMessage = "Este campo no puede exceder de los 100 caracteres.")]
    public string Asunto { get; set; } = string.Empty;

    [Required(ErrorMessage ="Este campo es requerido.")]
    [StringLength(100, ErrorMessage ="Este campo no puede exceder de los 100 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage ="Este campo es requerido.")]
    public int? Tiempo { get; set; }

    [Required(ErrorMessage = "El tecnico es requerido.")]
    public int TecnicoId { get; set; }

    [ForeignKey("TecnicoId")]
    public Tecnicos? Tecnico { get; set; }
}