using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Traveless.Services
{
    public class FlightsManager
    {
        internal List<Flights> flights = new List<Flights>();

        internal void LoadFromCSV()
        {
            flights.Clear();
            string csvFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "data", "flights.csv");

            try
            {
                using (var sr = new StreamReader(csvFile))
                using(var csv = new CsvReader(sr, CultureInfo.InvariantCulture)) 
                {
                    while (csv.Read())
                    {
                        flights.Add(new Flights(csv.GetField(0), csv.GetField(1), csv.GetField(2), csv.GetField(3), csv.GetField(4), csv.GetField(5), Int32.Parse(csv.GetField(6)), float.Parse(csv.GetField(7))));
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV File: {ex.Message}");
            }
        }
        internal List<Flights> searchFlights(string departure, string arrival, string day)
        {
            departure = departure.ToUpper();
            arrival = arrival.ToUpper();
            day = day.ToLower();
            List<Flights> foundFlights = new List<Flights>();
            foreach (Flights flight in flights)
            {
                if (flight.FromPort == departure || departure == "ANY")
                {
                    if (flight.ToPort == arrival || arrival == "ANY")
                    {
                        if (flight.DayofWeek == day || day == "any")
                        {
                            foundFlights.Add(flight);
                        }
                    }
                }
            }
            return foundFlights;
        }

        internal Flights searchFlights(string flightID)
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
                string csvFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "data", "flights.csv");
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
