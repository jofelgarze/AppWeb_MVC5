using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Pedido
    {
        public Pedido()
        {
            Detalles = new List<DetallePedido>();
        }
        public int Id { get; set; }

        [Required]

        public DateTime? FechaPedido { get; set; }

        [Required]
        public Cliente Cliente { get; set; }

        public List<DetallePedido> Detalles { get; set; }

        [NotMapped]
        public decimal Total 
        {
            get
            {
                decimal total = 0;
                if (Detalles != null && Detalles.Count > 0)
                {
                    total = Detalles.Sum(d => d.Producto.Precio * d.Cantidad);
                }
                return total;
            }
        }
    }

    public class DetallePedido {

        public int Id { get; set; }
        [Required]
        public Pedido Pedido { get; set; }
        [Required]
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }


    }
}