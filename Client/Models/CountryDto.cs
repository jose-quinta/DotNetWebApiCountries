using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models {
    public class CountryDto {
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;
    }
}