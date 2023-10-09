using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.Model
{
    public class Auditable
    {
        [Column(TypeName = "datetimeoffset")]
        public DateTimeOffset? Data { get; set; }

    }
}
