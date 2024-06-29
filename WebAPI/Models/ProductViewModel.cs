using System;

namespace WebAPI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime FabricationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int? SupplierId { get; set; }

    }
}
