using SharedLib.Codes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLib.Entities {
    [RestInPeaceEntity]
    [Table("admin_users")]
    public class EAdminUser {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime? CreationDateUTC { get; set; }
        public DateTime? ModificationDateUTC { get; set; }
    }
}
