using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightService.FlightFilters;

namespace FlightService
{
    public class FlightService
    {
        private FlightBuilder _flightBuilder;

        public FlightService()
        {
            _flightBuilder = new FlightBuilder();
        }
        public IList<Flight> GetFlights(IList<IFlightFilter> filters)
        {
            var flights = _flightBuilder.GetFlights().Where(f => IsFlightValid(f));

            return flights.Where(flight => filters.All(filter => filter.Evaluate(flight))).ToList();
        }

        public bool IsFlightValid(Flight flight)
        {
            var isValid = true;

            Segment previousSegment = null;
            foreach (var currentSegment in flight.Segments.OrderBy(s => s.DepartureDate))
            {
                // if arrival date is before departure date in the same segment, it is an invalid flight
                if (currentSegment.ArrivalDate <= currentSegment.DepartureDate)
                {
                    isValid = false;
                    break;
                }

                if (previousSegment != null && previousSegment.ArrivalDate > currentSegment.DepartureDate)
                {
                    isValid = false;
                    break;
                }
                previousSegment = currentSegment;
            }

            return isValid;
        }
    }
}
