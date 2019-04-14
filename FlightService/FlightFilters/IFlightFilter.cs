namespace FlightService.FlightFilters
{
    public interface IFlightFilter
    {
        bool Evaluate(Flight flight);
    }
}
