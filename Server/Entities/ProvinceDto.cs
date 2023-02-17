using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class ProvinceDto
    {
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
    }
}