using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.Model
{
    public class Postagem : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR")]
        [StringLength(1000)]
        public string Texto { get; set; } = string.Empty;

        public virtual Tema? Tema { get; set; }

        public virtual User? Usuario { get; set; }
    }
}
