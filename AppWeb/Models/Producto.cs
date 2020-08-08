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
        public decimal Precio { get; set; }
    }
}