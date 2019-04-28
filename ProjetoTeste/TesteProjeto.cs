using ProjetoAPI.Models;
using System;
using Xunit;

namespace ProjetoTeste
{
    public class TesteProjeto
    {
       

        [Fact]
        public void Test1()
        {

            var model = new Usuario();
            model.Nome = "Adriane Cibele";
            model.Telefone = "97920004";
            model.Bairro = "Cajuru";
            model.Cep = "829770998";
            model.Cidade = new Cidade();
            model.Cidade.IdCidade = 708;
            model.Email = "adricibelle@gmial.com";
            model.Logradouro = "Rua Coronel";
            model.Cep = "829770998";
            model.LogradouroNumero = "09";

            UsuarioValidacao.ValidaUsuario(model);


        }
    }
}
