using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoAPI.Models;
using ProjetoWeb.Models;
using ProjetoWeb.Services;

namespace ProjetoWeb.Controllers
{
    public class HomeController : Controller
    {
      
        //Service Cidade
        private CidadeService  _cidadeService;

        //Caminho para a API
        string BaseUrl = "https://localhost:44367";


        public HomeController(CidadeService cidadeService)
        {
            _cidadeService = cidadeService;

        }


        public IActionResult Index(string message = null)
        {
            ViewData["Message"] = "Lista de Usuários";

            // Mensagem de retorno das ações
            ViewData["MessageRetorno"] = message;

            var listModel = new List<Usuario>();

            //Chamada para a API
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);

                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");

                cliente.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = cliente.GetAsync("api/Usuario").Result;

                if(response.IsSuccessStatusCode)
                {
                    var usuarioResponse = response.Content.ReadAsStringAsync().Result;
                    listModel = JsonConvert.DeserializeObject<List<Usuario>>(usuarioResponse);
                }
            }

                return View(listModel);
        }


        [HttpGet]
        public IActionResult ListCidade(int id)
        {
            //chamada para o servise, retorna uma lista de Cidades 
            var list = View(_cidadeService.ListCidades(id));    

            return PartialView("_Cidade",list.Model);
        }

        //Inluir ou alterar Usuario
        [HttpPost]
        public IActionResult Manager(Usuario model)
        {
            var mensagemRetorno = "";

            if(model.IdUsuario > 0)
            {
                mensagemRetorno = "Usuário alterado com Sucesso!";
            }
            else
            {
                mensagemRetorno = "Usuário cadastrado com Sucesso!";
            }
       
            //Chamada para a API
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);

                string usuarioSerializado = JsonConvert.SerializeObject(model);

                var conteudoDados = new StringContent(usuarioSerializado, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = cliente.PostAsync("api/Usuario", conteudoDados).Result;

                if (response.IsSuccessStatusCode)
                {
                    var usuarioResponse = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<Usuario>(usuarioResponse);
                }
            }

            return RedirectToAction(nameof(Index), new {message= mensagemRetorno });
        }

        //Chamada para carregar a tela de inser ou update
        public IActionResult CadastroUsuarios(int id = 0)
        {

            var model = new Usuario();

            if (id > 0)
            { 

                using (HttpClient cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(BaseUrl);

                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");

                    cliente.DefaultRequestHeaders.Accept.Add(contentType);

                    HttpResponseMessage response = cliente.GetAsync("api/Usuario/" + id).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var usuarioResponse = response.Content.ReadAsStringAsync().Result;
                        model = JsonConvert.DeserializeObject<Usuario>(usuarioResponse);
                    }
                }
            }

            ViewData["Message"] = "Cadastro de Usuários";

            return View(model);
        }

        //Delete Usuario
        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);

 
                HttpResponseMessage response = cliente.DeleteAsync("api/Usuario/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var usuarioResponse = response.Content.ReadAsStringAsync().Result;                    
                }
            }

            return RedirectToAction(nameof(Index), new { message = "Excluido com sucesso!" });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
