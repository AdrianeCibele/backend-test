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
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeRepositorio _repo;

        public CidadeController(ICidadeRepositorio repo)
        {
            _repo = repo;
        }

        // GET: api/Usuarios
        [HttpGet]
        [HttpGet("{id}")]
        public IActionResult GetCidades(int id)
        {

            var result = _repo.GetAll(id);
            return Ok(result);
        }        

    }
}