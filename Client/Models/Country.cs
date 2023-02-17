using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models {
    public class Country {
        [Key]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Provinces")]
        public List<Province> Provices { get; set; } = new List<Province>();
    }
}