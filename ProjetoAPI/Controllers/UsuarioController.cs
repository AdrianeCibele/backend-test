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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repo;

        public UsuarioController(IUsuarioRepositorio repo)
        {
            _repo = repo;
        }

        // GET: api/Usuarios
        [HttpGet]
        public IActionResult GetUsuariosTeste()
        {

            var result = _repo.GetAll();
            return Ok(result);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public IActionResult GetUsuario([FromRoute] int id)
        {
            var usuario = _repo.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // Post: api/Usuarios/Add
        [HttpPost]
        public IActionResult Add([FromBody] Usuario model)
        {
            if (model == null)
                return BadRequest();

            int status;

            UsuarioValidacao.ValidaUsuario(model);

            if (model.IdUsuario > 0)
            {
                status = _repo.Update(model);
            }
            else
            {
                status = _repo.Add(model);
            }

            if (status != 1)
                return StatusCode(500, "Erro");

            return NoContent();
        }

        // GET: api/Usuarios/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repo.Delete(id);

            return Ok();
        }


    }
}