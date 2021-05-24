using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaLombriz.Clases
{
    public class Informacion
    {
        [Required]
        [MaxLength(140)]
        public string transactionAmount { get; set; }
        [Required]
        [MaxLength(140)]
        public string correo { get; set; }
        [Required]
        [MaxLength(140)]
        public string description { get; set; }
        [Required]
        [MaxLength(140)]
        public string token { get; set; }
        [Required]
        [MaxLength(140)]
        public string issuer_id { get; set; }
        [Required]
        [MaxLength(140)]
        public string installments { get; set; }
        [Required]
        [MaxLength(140)]
        public string payment_method_id { get; set; }
    }
}