using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Cliente
    {
        [Display(Name = "Identificación")]
        [Key]
        [MaxLength(15)]
        [MinLength(10)]
        [Column(TypeName = "varchar")]
        public string Identificacion { get; set; }

        [Display(Name = "Tipo Ident.")]
        [Required]
        public ETipoIdentificacion TipoIdentificacion { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required]
        [MinLength(5)]
        [MaxLength(300)]
        [Index(IsUnique = true)]
        public string NombreCompleto { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}