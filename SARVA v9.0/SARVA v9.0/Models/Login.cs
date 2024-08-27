using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SARVA.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}