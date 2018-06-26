using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestauranteApi.Models
{
    public class Prato
    {

        [Key]
        public int IdPrato { get; set; }

        public string Descricao { get; set; }

        public int IdRestaurante { get; set; }

        public decimal Preco { get; set; }

        [ForeignKey("IdRestaurante")]
        public Restaurante Restaurante { get; set; }

    }
}