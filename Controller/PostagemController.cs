using BlogPessoal.Model;
using BlogPessoal.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controller
{
    [Route("/postagens")]
    [ApiController]
    public class PostagenController : ControllerBase
    {
        private readonly IPostagemService _postagemService;
        private readonly IValidator<Postagem> _postagemValidator;
        public PostagenController(
                IPostagemService postagemService,
                IValidator<Postagem> postagenValidator
        )
        {
            _postagemService = postagemService;
            _postagemValidator = postagenValidator;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _postagemService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id) { 
            var Resposta = await _postagemService.GetById(id);
            if (Resposta is null) {
                return NotFound();
            }
            return Ok(Resposta);
        }
        [HttpGet("titulo/{titulo}")]
        public async Task<ActionResult> GetByTitulo (string titulo)
        {
            return Ok(await _postagemService.GetByTitulo(titulo));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Postagem postagem) {
            var validarPostagem = await _postagemValidator.ValidateAsync(postagem);

            if (!validarPostagem.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);
            await _postagemService.Create(postagem);

            return CreatedAtAction(nameof(GetById), new { id = postagem.Id }, postagem);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Postagem postagem)
        {
            if (postagem.Id == 0)
                return BadRequest("Id da Postagem é invalida");

            var validarPostagem = await _postagemValidator.ValidateAsync(postagem);

            if (!validarPostagem.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);

            var Resposta = await _postagemService.Update(postagem);

            if (Resposta is null)
                return NotFound("Postagem não encontrada!");
            return Ok(Resposta);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var BuscarPostagem = await _postagemService.GetById(id);
            if (BuscarPostagem is null)
                return NotFound("Postagem não encontrada!");
            await _postagemService.Delete(BuscarPostagem);
            return NoContent();
        }
    }
}
