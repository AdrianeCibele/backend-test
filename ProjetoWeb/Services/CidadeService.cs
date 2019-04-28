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


namespace ProjetoWeb.Services
{
    public class CidadeService
    {

        string BaseUrl = "https://localhost:44367";

        public List<Cidade> ListCidades(int id)
        {
            var listModel = new List<Cidade>();

            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);

                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");

                cliente.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = cliente.GetAsync("api/Cidade/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var usuarioResponse = response.Content.ReadAsStringAsync().Result;
                    listModel = JsonConvert.DeserializeObject<List<Cidade>>(usuarioResponse);
                }

                return listModel;
            }
        }
    }
}
