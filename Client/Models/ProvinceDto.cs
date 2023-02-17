using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models {
    public class ProvinceDto {
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Country ID")]
        public int CountryId { get; set; }
    }
}