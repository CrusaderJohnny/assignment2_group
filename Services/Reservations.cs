using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Traveless.Services
{
    internal class Reservations
    {
        private string reservation_code;
        private string flightID;
        private string flightName;
        private string dayofWeek;
        private string timeofFlight;
        private float flightCost;
        private string clientName;
        private string clientCitizen;
        private bool activeReservation;

        public string Reservation_Code
        {
            get { return reservation_code; }
            set { reservation_code = value; }
        }
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
        public float FlightCost
        {
            get { return FlightCost; }
            set { FlightCost = value; }
        }
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }
        public string ClientCitizen
        {
            get { return clientCitizen; }
            set { clientCitizen = value; }
        }
        public bool ActiveReservation
        {
            get { return activeReservation; }
            set { activeReservation = value; }
        }
        internal Reservations(string flightID, string flightName, string dayofWeek, string timeofFlight, float flightCost, string clientName, string clientCitizen)
        {
            this.FlightID = flightID;
            this.FlightName = flightName;
            this.DayofWeek = dayofWeek;
            this.TimeofFlight = timeofFlight;
            this.FlightCost = flightCost;
            this.ClientName = clientName;
            this.clientCitizen = clientCitizen;
            this.ActiveReservation = true;
            RandomReservationCode();
        }
        [JsonConstructor]
        internal Reservations(string reservation_code, string flightID, string flightName, string dayofWeek, string timeofFlight, float flightCost, string clientName, string clientCitizen, bool activeReservation)
        {
            this.Reservation_Code = reservation_code;
            this.FlightID = flightID;
            this.FlightName = flightName;
            this.DayofWeek = dayofWeek;
            this.TimeofFlight = timeofFlight;
            this.FlightCost = flightCost;
            this.ClientName = clientName;
            this.clientCitizen= clientCitizen;
            this.ActiveReservation = activeReservation;
        }
        internal Reservations() 
        {

        }
        internal void RandomReservationCode()
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVXYZ".ToCharArray();
            string reservation_code = chars[RandomNumberGenerator.GetInt32(chars.Count())].ToString();
            for (int i = 0; i < 5; i++)
            {
                reservation_code += RandomNumberGenerator.GetInt32(9).ToString();
            } 
            this.Reservation_Code = reservation_code;
        }
        public override string ToString()
        {
            return $"{Reservation_Code}, {ClientName}, {ClientCitizen}, {FlightID}, {FlightName}, {DayofWeek}, {TimeofFlight}, ${FlightCost}";
        }
    }
}
