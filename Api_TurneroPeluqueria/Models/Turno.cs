using Api_TurneroPeluqueria.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Turno
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column("id_cliente")]  // Mapea al nombre de la columna en la base de datos
    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    public Cliente Cliente { get; set; }

    [Required]
    [Column("id_servicio")]  // Mapea al nombre de la columna en la base de datos
    public int ServicioId { get; set; }

    [ForeignKey("ServicioId")]
    public Servicio Servicio { get; set; }

    [Required]
    [Column("id_empleado")]  // Mapea al nombre de la columna en la base de datos
    public int EmpleadoId { get; set; }

    [ForeignKey("EmpleadoId")]
    public Empleado Empleado { get; set; }

    [Required(ErrorMessage = "La fecha y hora del turno es obligatoria")]
    [Column("fecha_hora")]  // Mapea al nombre de la columna en la base de datos
    public DateTime FechaHora { get; set; }

    [Required]
    [StringLength(50)]
    [Column("estado")]  // Mapea al nombre de la columna en la base de datos
    public string Estado { get; set; } = "Pendiente";

    [Column("observaciones")]  // Mapea al nombre de la columna en la base de datos
    public string Observaciones { get; set; }
}
