using BlogPessoal.Model;
using BlogPessoal.Service;
using BlogPessoal.Service.Implements;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controller
{
    [Authorize]
    [Route("~/temas")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _temaService;
        private readonly IValidator<Tema> _temaValidator;
        public TemaController(
                ITemaService temaService,
                IValidator<Tema> temaValidator
        )
        {
            _temaService = temaService;
            _temaValidator = temaValidator;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _temaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id) { 
            var Resposta = await _temaService.GetById(id);
            if (Resposta is null) {
                return NotFound();
            }
            return Ok(Resposta);
        }
        [HttpGet("descricao/{descricao}")]
        public async Task<ActionResult> GetByDescricao (string descricao)
        {
            return Ok(await _temaService.GetByDescricao(descricao));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Tema Tema) {
            var validarTema = await _temaValidator.ValidateAsync(Tema);

            if (!validarTema.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarTema);
            await _temaService.Create(Tema);

            return CreatedAtAction(nameof(GetById), new { id = Tema.Id }, Tema);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Tema Tema)
        {
            if (Tema.Id == 0)
                return BadRequest("Id da Tema é invalida");

            var validarTema = await _temaValidator.ValidateAsync(Tema);

            if (!validarTema.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarTema);

            var Resposta = await _temaService.Update(Tema);

            if (Resposta is null)
                return NotFound("Tema não encontrada!");
            return Ok(Resposta);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var BuscarTema = await _temaService.GetById(id);
            if (BuscarTema is null)
                return NotFound("Tema não encontrada!");
            await _temaService.Delete(BuscarTema);
            return NoContent();
        }
    }
}
