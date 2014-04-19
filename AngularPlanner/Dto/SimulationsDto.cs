using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularPlanner.Dto
{
    public class SimulationsDto
    {
        public IEnumerable<string> Dates { get; set; }

        public IEnumerable<decimal> PastIncomes { get; set; }
        public IEnumerable<decimal> PastOutcomes { get; set; }

        public decimal EstimatedIncome { get; set; }
        public decimal EstimatedOutcome { get; set; }

        public SimulationScope Scope { get; set; }
    }

    public enum SimulationScope
    {
        Daily = 1,
        Monthly = 2,
        Yearly = 3
    }
}