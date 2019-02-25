using Microsoft.EntityFrameworkCore;
using OddestOdds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using OddestOdds.Infrastructure.Entities;

namespace OddestOdds.Infrastructure.Data
{
    public class OddestOddsContext : DbContext
    {
        public OddestOddsContext(DbContextOptions<OddestOddsContext> options)
            : base(options)
        {
            
        }

        public DbSet<Odd> Odds { get; set; }
        public DbSet<OddValue> OddValues { get; set; }
    }
}
