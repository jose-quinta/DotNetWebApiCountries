using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Province> Provices { get; set; } = new List<Province>();
    }
}