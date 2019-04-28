using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Models;

namespace ProjetoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoRepositorio _repo;

        public EstadoController(IEstadoRepositorio repo)
        {
            _repo = repo;
        }

        // GET: api/Usuarios
        [HttpGet]
        public IActionResult GetEstados()
        {
            var result = _repo.GetAll();
            return Ok(result);
        }        

    }
}