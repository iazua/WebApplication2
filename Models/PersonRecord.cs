namespace WebApplication2.Models                     // ← ajusta
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TB_Base_Dot_CCR_IN_EX")]
    public class PersonRecord
    {
        [Key]
        [Column("Rut / DNI")]
        [StringLength(50)]
        public string RutDni { get; set; } = default!;

        [Column("Agente")] public string? Agente { get; set; }
        [Column("Pais_Call_Center")] public string? PaisCallCenter { get; set; }
        [Column("Nombre_Call_Center")] public string? NombreCallCenter { get; set; }
        [Column("Area")] public string? Area { get; set; }
        [Column("AreaGestion")] public string? AreaGestion { get; set; }
        [Column("Contrato")] public double? Contrato { get; set; }
        [Column("Tipos de agente")] public string? TiposDeAgente { get; set; }
        [Column("Servicio")] public string? Servicio { get; set; }
        [Column("Usuarios RcWeb")] public string? UsuariosRcWeb { get; set; }
        [Column("Nombre Jefatura")] public string? NombreJefatura { get; set; }
        [Column("Rut_Ficticio")] public string? RutFicticio { get; set; }
        [Column("DV")] public string? DV { get; set; }
        [Column("Clasifica_Cargo")] public string? ClasificaCargo { get; set; }
        [Column("CARGO")] public string? Cargo { get; set; }

        [Column("Fecha Ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaIngreso { get; set; }

        [Column("Fecha Baja")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaBaja { get; set; }

        [Column("N° Personal")] public string? NumeroPersonal { get; set; }

        [Column("Correo")]
        [EmailAddress] public string? Correo { get; set; }

        [Column("Mes_Gestion")] public string? MesGestion { get; set; }
        [Column("Usuario_Genesys")] public string? UsuarioGenesys { get; set; }
    }
}
