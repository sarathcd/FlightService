using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FlightService;
using FlightService.FlightFilters;

namespace FlightServiceTest
{
    [TestClass]
    public class FlightServiceTest
    {
        private FlightService.FlightService _service;

        public FlightServiceTest()
        {
            _service = new FlightService.FlightService();
        }

        [TestMethod]
        public void DepartureFilterTest()
        {
            var flights = _service.GetFlights(new List<IFlightFilter> { new DepartureFlightFilter(DateTime.Now, FromWhen.Before) });
            Assert.AreEqual(flights.Count, 1);
        }

        [TestMethod]
        public void ValidationTest()
        {
            var flights = (new FlightBuilder()).GetFlights();

            var validFlights = flights.Where(f => _service.IsFlightValid(f)).ToList();

            Assert.AreEqual(validFlights.Count, 5);
        }

        [TestMethod]
        public void TransitTimeFilterTest()
        {
            var flights = _service.GetFlights(new List<IFlightFilter> { new TransitTimeFlightFilter(TimeSpan.FromHours(2)) });
            Assert.AreEqual(flights.Count, 1);
        }
    }
}
