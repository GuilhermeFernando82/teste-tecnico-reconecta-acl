using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apivscode2.Models
{
    public class ProducerResponseDTO
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public string Document { get; set;}
        public string Status { get; set;}
        public DateTime DateRegistration { get; set; }
    }
}