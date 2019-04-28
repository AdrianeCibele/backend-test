using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Models
{  
    public class Cidade
    {
        public int IdCidade { get; set; }
        public string Nome { get; set; }
        public int IdEstado { get; set; }

    }

}
