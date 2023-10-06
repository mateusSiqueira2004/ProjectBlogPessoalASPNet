using BlogPessoal.Model;
using FluentValidation;

namespace BlogPessoal.Validator
{
    public class TemaValidator : AbstractValidator<Tema>
    {
        public TemaValidator() {
            RuleFor(p => p.Descricao)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(255);
        }
    }
}
