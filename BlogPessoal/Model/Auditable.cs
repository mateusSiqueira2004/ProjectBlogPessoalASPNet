using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.Model
{
    public class Auditable
    {
        public DateTimeOffset? Data { get; set; }

    }
}
