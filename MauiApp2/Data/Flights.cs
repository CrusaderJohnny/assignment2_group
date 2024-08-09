using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Data
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
        private double flightCost;
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
        public double FlightCost
        {
            get { return flightCost; }
            set { flightCost = value; }
        }


        public Flights(string flightID, string flightName, string fromPort, string toPort, string dayofWeek, string timeofFlight, int freeSeats, double flightCost)
        {
            this.flightID = flightID;
            this.flightName = flightName;
            this.fromPort = fromPort;
            this.toPort = toPort;
            this.dayofWeek = dayofWeek;
            this.timeofFlight = timeofFlight;
            this.freeSeats = freeSeats;
            this.flightCost = flightCost;
        }
        public Flights()
        {

        }
        public override string ToString()
        {
            if(flightID == null)
            {
                return "Loading...";
            }
            return $"Flight ID: {flightID}, Flight Name: {flightName}, From Airport: {fromPort}, To Airport: {toPort}, Day: {dayofWeek}, Time: {timeofFlight}, Seats Available: {freeSeats}, Cost: {flightCost}";
            
        }
    }
}
