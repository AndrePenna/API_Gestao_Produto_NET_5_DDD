using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Supplier")]
    public class Supplier
    {
        [Column("id", Order = 1)]
        public int Id { get; set; }

        [Column("Name")]
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [Column("Document")]
        [MaxLength(18)]
        [Required]
        public string Document { get; set; }
    }
}
