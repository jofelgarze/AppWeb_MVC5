using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName="varchar")]
        public string Nombre { get; set; }

        [Required]
        [Range(minimum: 1, maximum: 9999.99)]
        public decimal Precio { get; set; }

        [Display(Name = "F. Aprobación")]
        [Column(TypeName = "date")]
        public DateTime? FechaAprobacion { get; set; }
    }
}