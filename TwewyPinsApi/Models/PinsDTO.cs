using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwewyPinsApi.Models
{
    public class PinsDTO
    {
        public long Id { get; set; }
        public string Info { get; set; }
        public bool Mutation { get; set; }
        public string User { get; set; }
    }
}
