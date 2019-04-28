using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Models
{
    public interface IUsuarioRepositorio
    {
        IEnumerable<Usuario> GetAll();

        Usuario Get(int Id);

        int Add(Usuario model);

        int Delete(int Id);

        int Update(Usuario model);
    }
}
