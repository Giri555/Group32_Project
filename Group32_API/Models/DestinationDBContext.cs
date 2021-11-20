using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Group32_API.Models
{
    public class DestinationDBContext : DbContext
    {
        public DestinationDBContext(DbContextOptions<DestinationDBContext> options) : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; }
    }
}
