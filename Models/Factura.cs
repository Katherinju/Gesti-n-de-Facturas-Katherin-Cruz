using System;
using System.ComponentModel.DataAnnotations;

namespace GestionFacturas.Models
{
    public class Factura
    {
        public int Id { get; set; }  // Entity Framework Core reconocerá esto como la clave primaria

        [Required(ErrorMessage = "El número de factura es obligatorio.")]
        [Display(Name = "Número de Factura")]
        public required string NumeroFactura { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Vencimiento")]
        public DateTime FechaVencimiento { get; set; }



        // Propiedad calculada para el estado de la factura
        [Display(Name = "Estado")]
        public string Estado
        {
            get
            {
                if (FechaVencimiento < DateTime.Today)
                    return "Vencida";
                if ((FechaVencimiento - DateTime.Today).TotalDays <= 3)
                    return "Próxima a vencer";
                return "Vigente";
            }
        }

        // 🔹 Constructor para inicializar `NumeroFactura`
        public Factura()
        {
            NumeroFactura = string.Empty; // O un valor predeterminado como "FACT-0001"
        }
    }
}