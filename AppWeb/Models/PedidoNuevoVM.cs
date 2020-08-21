using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class PedidoNuevoVM
    {
        public PedidoNuevoVM()
        {
            Detalles = new List<DetallePedidoVM>();
        }

        [Required]
        [JsonProperty]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? FechaPedido { get; set; }

        [Required]
        public string ClienteId { get; set; }

        [Required]
        public string ClienteNombre { get; set; }

        public List<DetallePedidoVM> Detalles { get; set; }
        
        [NotMapped]
        public decimal Total
        {
            get
            {
                decimal total = 0;
                if (Detalles != null && Detalles.Count > 0)
                {
                    total = Detalles.Sum(d => d.Precio * d.Cantidad);
                }
                return total;
            }
        }
    }

    public class DetallePedidoVM {
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public string ProductoNombre { get; set; }
        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [NotMapped]
        public decimal Total
        {
            get
            {                
                return Precio * Cantidad;
            }
        }
    }
}