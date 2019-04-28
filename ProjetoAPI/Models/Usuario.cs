using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Models
{


    public class Usuario
    {
       
        public int IdUsuario { get; set; }



        public string Nome { get; set; }

 

        public string Email { get; set; }



        public string Telefone { get; set; }



        public string Logradouro { get; set; }



        public string LogradouroNumero { get; set; }


 
        public string  Bairro { get; set; }


        public Estado Estado { get; set; }


        public Cidade Cidade { get; set; }



        public string Cep { get; set; }

        public bool AtivoUsuario { get; set; }


        /// <summary>
        /// IsValid field - form validation
        /// </summary>
        public bool IsValidField { get; set; }

        /// <summary>
        /// IsValid field title - form validation
        /// </summary>
        public string IsValidFieldTitle { get; set; }

        /// <summary>
        /// IsValid field message - form validation
        /// </summary>
        public string IsValidFieldMessage { get; set; }

        /// <summary>
        /// IsValid field IsValidFieldCode - form validation
        /// </summary>
        public string IsValidFieldCode { get; set; }
    }
}
