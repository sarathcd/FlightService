using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightService.FlightFilters
{
    public class ArrivalFlightFilter : IFlightFilter
    {
        public FromWhen FromWhen { get; private set; }

        public DateTime ArrivalTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ArrivalFlightFilter(DateTime arrivalTime, FromWhen fromWhen)
        {
            ArrivalTime = arrivalTime;
            FromWhen = fromWhen;
        }

        public bool Evaluate(Flight flight)
        {
            if (this.FromWhen == FromWhen.Before)
                return flight.Segments.OrderByDescending(s => s.DepartureDate).First().DepartureDate <= this.ArrivalTime;

            return flight.Segments.OrderByDescending(s => s.DepartureDate).First().DepartureDate >= this.ArrivalTime;
        }
    }
}
