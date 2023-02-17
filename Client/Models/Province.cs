using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models {
    public class Province {
        [Key]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("CountryId")]
        public int CountryId { get; set; }
        [DisplayName("Country")]
        public Country Country { get; set; } = default!;
    }
}