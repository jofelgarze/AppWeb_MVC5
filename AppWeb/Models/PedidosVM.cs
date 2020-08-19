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
        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; }
    }
}