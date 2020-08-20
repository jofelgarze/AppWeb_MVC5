using AppWeb.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class PedidosVM
    {

        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        [Display(Name = "Fecha Pedido")]
        [JsonProperty]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; }
    }
}