using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace Tarea1.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "Este campo es requerido")]

    public DateTime FechaIngreso { get; set; }

    [Required(ErrorMessage = "El nombre es requerido.")]
    [StringLength(100, ErrorMessage ="El nombre no puede exceder de los 100 caracteres.")]
    public string Nombres { get; set; }

    public string Direccion { get; set; }


    [Required(ErrorMessage = "El RNC es requerido.")]
    [StringLength(9, ErrorMessage = "El RNC debe ser un número positivo y no puede exceder de 9 dígitos.")]
    public string RNC { get; set; }

    [Required(ErrorMessage = "El limite de credito es requerido")]
    public int LimiteCredito { get; set; }

    [Required(ErrorMessage = "El tecnico es requerido.")]
    public int TecnicoId { get; set; }

    [ForeignKey("TecnicoId")]
    public Tecnicos Tecnico { get; set; }




}
