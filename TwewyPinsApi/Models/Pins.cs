using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwewyPinsApi.Models
{
    public class Pins
    {
        [Key]
        public int Id { get; set; }
        public string Info { get; set; }
        public bool Mutation { get; set; }
        public string PinUser { get; set; }
        public string PinNumber { get; set; }
        public string Name { get; set; }
        //public string Secret { get; set; }
    }
}
