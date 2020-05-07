using SharedLib.Codes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLib.Entities {
    [RestInPeaceEntity]
    [Table("products")]
    public class EProduct {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public Decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateUTC { get; set; }
        public DateTime ModificationDateUTC { get; set; }
        [NotMapped]
        public double Amount { get; set; }
        [NotMapped]
        public Decimal TotalPrice { get; set; }
    }
}
