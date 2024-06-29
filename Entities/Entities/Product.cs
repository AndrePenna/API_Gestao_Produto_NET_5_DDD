using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Product")]
    public class Product
    {
        [Column("id", Order = 1)]
        public int Id { get; set; }

        [Column("Description")]
        [MaxLength(250)]
        [Required]
        public string Description { get; set; }

        [Column("IsActive")]
        [DefaultValue(true)]
        public bool? IsActive { get; set; }

        [Column("FabricationDate")]
        public DateTime? FabricationDate { get; set; }

        [Column("ExpirationDate")]
        public DateTime? ExpirationDate { get; set; }

        [ForeignKey("Supplier")]
        [Column(Order = 1)]
        public int? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
