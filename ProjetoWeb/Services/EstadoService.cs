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
    public class EstadoService
    {

        string BaseUrl = "https://localhost:44367";

        public List<Estado> ListEstados()
        {
            var listModel = new List<Estado>();

            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);

                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");

                cliente.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = cliente.GetAsync("api/Estado").Result;

                if (response.IsSuccessStatusCode)
                {
                    var usuarioResponse = response.Content.ReadAsStringAsync().Result;
                    listModel = JsonConvert.DeserializeObject<List<Estado>>(usuarioResponse);
                }

                return listModel;
            }
        }
    }
}
