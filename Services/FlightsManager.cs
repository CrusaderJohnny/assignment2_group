using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless.Services
{
    public class FlightsManager
    {
        internal List<Flights> flights = new List<Flights>();
        internal string csvFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "raw", "flights.csv");

        internal void LoadFromCSV()
        {
            flights.Clear();
            try
            {
                using (var sr = new StreamReader(csvFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        if (values.Length == 8)
                        {
                            flights.Add(new Flights(values[0], values[1], values[2], values[3], values[4], values[5], Int32.Parse(values[6]), double.Parse(values[7])));
                        }
                        else
                        {
                            Console.WriteLine($"Invalid Data in line: {line}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV File: {ex.Message}");
            }
        }
        internal List<Flights> findFlights(string departure, string arrival, string day)
        {
            departure = departure.ToUpper();
            arrival = arrival.ToUpper();
            day = day.ToLower();
            List<Flights> foundFlights = new List<Flights>();
            foreach (Flights flight in flights)
            {
                if (flight.FromPort == departure || departure == "ANY")
                {
                    if (flight.ToPort == arrival || departure == "ANY")
                    {
                        if (flight.DayofWeek == day || day == "ANY")
                        {
                            foundFlights.Add(flight);
                        }
                    }
                }
            }
            return foundFlights;
        }

        internal Flights findFlights(string flightID)
        {
            flightID = flightID.ToUpper();
            foreach (Flights flight in flights)
            {
                if (flight.FlightID == flightID)
                {
                    return flight;
                }
            }
            return new Flights();
        }

        internal void SeatReservation(string flightID)
        {
            flightID = flightID.ToUpper();
            bool flightFound = false;
            foreach (Flights flight in flights)
            {
                if (flight.FlightID == flightID)
                {
                    flightFound = true;
                    flight.FreeSeats--;
                    break;
                }
            }
            if (flightFound)
            {
                SaveFlights();
            }
        }

        internal void UnreserveSeat(string flightID)
        {
            flightID = flightID.ToUpper();
            bool unreserveFound = false;
            foreach (Flights flight in flights)
            {
                if (flight.FlightID == flightID)
                {
                    unreserveFound = true;
                    flight.FreeSeats++;
                    break;
                }
            }
            if (unreserveFound)
            {
                SaveFlights();
            }
        }

        internal void SaveFlights()
        {
            try
            {
                List<string> savedFlights = new List<string>();
                foreach (Flights flight in flights)
                {
                    string[] items = [flight.FlightID, flight.FlightName, flight.FromPort, flight.ToPort, flight.DayofWeek, flight.TimeofFlight, flight.FreeSeats.ToString(), flight.FlightCost.ToString()];
                    savedFlights.Add(string.Join(",", items));
                }
                if (savedFlights.Count() > 0)
                {
                    File.WriteAllLines(csvFile, savedFlights);
                    LoadFromCSV();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Reading CSV file: {ex.Message}");
                return;
            }
        }
    }
}
