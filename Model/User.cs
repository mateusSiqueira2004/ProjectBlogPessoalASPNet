using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string? Nome { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string? Usuario { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string? Senha { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(510)]
        public string? Foto { get; set;}

        [InverseProperty("Usuario")]
        public ICollection<Postagem>? Postagem { get; set; }
    }
}
