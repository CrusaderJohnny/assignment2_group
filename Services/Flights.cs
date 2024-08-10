using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveless.Services
{
    internal class Flights
    {
        private string flightID;
        private string flightName;
        private string fromPort;
        private string toPort;
        private string dayofWeek;
        private string timeofFlight;
        private int freeSeats;
        private float flightCost;
        public string FlightID
        {
            get { return flightID; }
            set { flightID = value; }
        }
        public string FlightName
        {
            get { return flightName; }
            set { flightName = value; }
        }
        public string FromPort
        {
            get { return fromPort; }
            set { fromPort = value; }
        }
        public string ToPort
        {
            get { return toPort; }
            set { toPort = value; }
        }
        public string DayofWeek
        {
            get { return dayofWeek; }
            set { dayofWeek = value; }
        }
        public string TimeofFlight
        {
            get { return timeofFlight; }
            set { timeofFlight = value; }
        }
        public int FreeSeats
        {
            get { return freeSeats; }
            set { freeSeats = value; }
        }
        public float FlightCost
        {
            get { return flightCost; }
            set { flightCost = value; }
        }


        public Flights(string flightID, string flightName, string fromPort, string toPort, string dayofWeek, string timeofFlight, int freeSeats, float flightCost)
        {
            this.FlightID = flightID;
            this.FlightName = flightName;
            this.FromPort = fromPort;
            this.ToPort = toPort;
            this.DayofWeek = dayofWeek;
            this.TimeofFlight = timeofFlight;
            this.FreeSeats = freeSeats;
            this.FlightCost = flightCost;
        }
        public Flights()
        {

        }
        public override string ToString()
        {
            if (flightID == null)
            {
                return "";
            }
            return $"{FlightID}, {FlightName}, {FromPort}, {ToPort}, {DayofWeek},  {TimeofFlight}, Seats Available: {FreeSeats}, Cost: ${FlightCost}";

        }
    }
}
