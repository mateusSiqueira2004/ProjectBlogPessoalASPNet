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
    }
}
