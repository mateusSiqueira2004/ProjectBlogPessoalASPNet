using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.Model
{
    public class UserLogin
    {
       
        public long Id { get; set; }

        public string? Nome { get; set; }

        public string? Usuario { get; set; }

        public string? Senha { get; set; }

        public string? Foto { get; set;}

        public string Token { get; set;} = string.Empty;

    }
}
