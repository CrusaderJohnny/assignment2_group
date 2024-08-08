using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Data
{
    public class Flights
    {
        internal string flightID;
        internal string flightName;
        internal string fromPort;
        internal string toPort;
        internal string dayofWeek;
        internal string timeofFlight;
        internal int flightDistance;
        internal double flightCost;

        public Flights(string flightID, string flightName, string fromPort, string toPort, string dayofWeek, string timeofFlight, int flightDistance, double flightCost)
        {
            this.flightID = flightID;
            this.flightName = flightName;
            this.fromPort = fromPort;
            this.toPort = toPort;
            this.dayofWeek = dayofWeek;
            this.timeofFlight = timeofFlight;
            this.flightDistance = flightDistance;
            this.flightCost = flightCost;
        }
        public Flights()
        {

        }
        public override string ToString()
        {
            return $"Flight ID: {flightID}, Flight Name: {flightName}, From Airport: {fromPort}, To Airport: {toPort}, Day: {dayofWeek}, Time: {timeofFlight}, Travel Distance: {flightDistance}, Cost: {flightCost}";
        }
        public string FromAirport
        {
            get { return fromPort ; }
        }
        public string ToAirport
        {
            get { return toPort; }
        }
        public string DayofWeek
        {
            get { return dayofWeek ; } 
        }
        public void SetFID(string fid)
        { 
            this.flightID = fid;
        }
        public void SetFName(string fname)
        {
            this.flightName = fname; 
        }
        public void SetPort(string port)
        {
            this.toPort = port;
        }
    }
}
