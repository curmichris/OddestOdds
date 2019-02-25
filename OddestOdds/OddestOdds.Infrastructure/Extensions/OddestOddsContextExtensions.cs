using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using OddestOdds.Core.Entities;
using OddestOdds.Infrastructure.Data;
using OddestOdds.Infrastructure.Entities;

namespace OddestOdds.Infrastructure.Extensions
{
    public static class OddestOddsContextExtensions
    {
        public static void EnsureSeedDataForContext(this OddestOddsContext context)
        {
            if (context.Odds.Any())
            {
                return;
            }

            var odds = new List<Odd>()
            {
                new Odd()
                {
                    HomeTeamName = "A.C. Milan",
                    AwayTeamName = "Juventus FC",
                    OddName = "A.C. Milan Vs Juventus Quarter Final",
                    OddValues = new OddValue()
                    {
                        HomeOddValue = new decimal(1.35),
                        AwayOddValue = new decimal(3.10),
                        DrawOddValue = new decimal(1.01)
                    },
                },
                new Odd()
                {
                    HomeTeamName = "Barcelona FC",
                    AwayTeamName = "Real Madrid FC",
                    OddName = "Barcelona Vs Real Madrid Quarter Final",
                    OddValues = new OddValue()
                    {
                        HomeOddValue = new decimal(1.22),
                        AwayOddValue = new decimal(3.0),
                        DrawOddValue = new decimal(1.01)
                    },
                },
                new Odd()
                {
                    HomeTeamName = "Celtic",
                    AwayTeamName = "Rangers",
                    OddName = "Celtic Vs Rangers League Match",
                    OddValues = new OddValue()
                    {
                        HomeOddValue = new decimal(2.66),
                        AwayOddValue = new decimal(1.0),
                        DrawOddValue = new decimal(3.43)
                    },
                },
            };

            context.Odds.AddRange(odds);
            context.SaveChanges();
        }
    }
}
