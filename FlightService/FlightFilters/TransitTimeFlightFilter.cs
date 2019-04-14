using System;
using System.Linq;

namespace FlightService.FlightFilters
{
    public class TransitTimeFlightFilter : IFlightFilter
    {
        public TimeSpan TransitTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public TransitTimeFlightFilter(TimeSpan transitTime)
        {
            TransitTime = transitTime;
        }

        public bool Evaluate(Flight flight)
        {
            var isValid = false;

            if (flight.Segments.Count() <= 1)
                return isValid;

            Segment previousSegment = null;
            foreach (var currentSegment in flight.Segments.OrderBy(s => s.DepartureDate))
            {
                if (previousSegment != null && currentSegment.DepartureDate.Subtract(previousSegment.ArrivalDate) > this.TransitTime)
                {
                    isValid = true;
                    break;
                }
                previousSegment = currentSegment;
            }

            return isValid;
        }
    }
}
