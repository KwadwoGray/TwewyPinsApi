using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TwewyPinsApi.Models
{
    public class PinsContext : DbContext
    { 
        public PinsContext(DbContextOptions<PinsContext> options)
            : base(options)
        {

        }

        public DbSet<Pins> PinMutations { get; set; }

        public List<Pins> getPins() => PinMutations.Local.ToList<Pins>();
    }
}
