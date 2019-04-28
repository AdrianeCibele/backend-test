using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Models
{
    public interface ICidadeRepositorio
    {
        IEnumerable<Cidade> GetAll(int idEstado);
    }
}
