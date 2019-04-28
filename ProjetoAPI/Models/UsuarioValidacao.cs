using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Models
{
    public static class UsuarioValidacao
    {
        //Valida o Nome do Usuario se é null
        public static void ValidaUsuario(Usuario model)
        {
            if(string.IsNullOrEmpty(model.Nome))
            {
                throw new Exception("O nome deve ser Informado");
            }

        }

    }
}
