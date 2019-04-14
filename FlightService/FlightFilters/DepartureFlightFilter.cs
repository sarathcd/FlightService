using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightService.FlightFilters
{
    /// <summary>
    /// Filters based on the departure date and time
    /// </summary>
    public class DepartureFlightFilter : IFlightFilter
    {
        public FromWhen FromWhen { get; private set; }

        public DateTime DepartureTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DepartureFlightFilter(DateTime departureTime, FromWhen fromWhen)
        {
            DepartureTime = departureTime;
            FromWhen = fromWhen;
        }

        public bool Evaluate(Flight flight)
        {
            if (this.FromWhen == FromWhen.Before)
                return flight.Segments.OrderBy(s => s.DepartureDate).First().DepartureDate <= this.DepartureTime;

            return flight.Segments.OrderBy(s => s.DepartureDate).First().DepartureDate >= this.DepartureTime;
        }
    }
}
