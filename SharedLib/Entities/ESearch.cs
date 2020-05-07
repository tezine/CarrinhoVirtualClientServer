using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SharedLib.Codes;

namespace SharedLib.Entities {
    [RestInPeaceEntity]
    [Table("searchs")]
    public class ESearch {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDateUTC { get; set; }
    }
}
