using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; } = default!;
    }
}