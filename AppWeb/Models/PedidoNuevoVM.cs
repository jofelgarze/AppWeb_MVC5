using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class PedidoNuevoVM
    {
        [Required]
        public DateTime? FechaPedido { get; set; }

        [Required]
        public string ClienteId { get; set; }

        [Required]
        public string ClienteNombre { get; set; }

        public List<DetallePedidoVM> Detalles { get; set; }
    }

    public class DetallePedidoVM {
        public int Id { get; set; }

        [Required]
        public string ProductoId { get; set; }

        [Required]
        public string ProductoNombre { get; set; }
        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}