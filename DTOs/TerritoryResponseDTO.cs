using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apivscode2.Models
{
    public class TerritoryResponseDTO
    {
        public int Id { get; set; }
        public int ProducerId { get; set;}
        public string Name { get; set;}
        public int Area { get; set;}
    }
}