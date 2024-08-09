using Android.Content.Res;
using MauiApp2.Components.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Android.Graphics.ImageDecoder;
using static Android.Resource;

/* This is the ReservationManagement class.
 * this class will hold all methods and attributes related to reservation management and creation,
 * along with reading and writing of cvs files for reservations.
 */

namespace MauiApp2.Data
{
    internal class ReservationManagement
    {
        /* all properties associated to reservations
         */
        private string reservation_code { get { return reservation_code; } set { reservation_code = value; } }
        private string flight_code { get { return flight_code; } set { flight_code = value; } }
        private string airline { get { return airline; } set { airline = value; } }
        private string cost { get { return cost; } set { cost = value; } }
        private string day { get { return day; } set { day = value; } }
        private string time { get { return time; } set { time = value; } }
        private string name { get { return name; } set { name = value; } }
        private string citizenship { get { return citizenship; } set { citizenship = value; } }
        private bool active { get { return active; } set { active = value; } }

        internal List<ReservationManagement>? reservations = new List<ReservationManagement>();

        internal ReservationManagement(string flight_code, string airline, string cost, string day, string time, string name, string citizenship)
        {
            this.flight_code = flight_code;
            this.airline = airline;
            this.cost = cost;
            this.day = day;
            this.time = time;
            this.name = name;
            this.citizenship = citizenship;
            this.active = true;
            CreateRC();
        }

        internal ReservationManagement(string reservation_code, string flight_code, string airline, string cost, string day, string time, string name, string citizenship, bool active)
        {
            this.reservation_code = reservation_code;
            this.flight_code = flight_code;
            this.airline = airline;
            this.cost = cost;
            this.day = day;
            this.time = time;
            this.name = name;
            this.citizenship = citizenship;
            this.active = active;
        }

        internal ReservationManagement()
        {
        }

        internal void CreateRC()
        {
            char[] codeList = "ADJKLTXYZ".ToCharArray();
            string reservation_code = "";
            reservation_code += codeList[RandomNumberGenerator.GetInt32(codeList.Count())].ToString();

            for (int counter = 0; counter < 4; counter++)
            {
                reservation_code += RandomNumberGenerator.GetInt32(9).ToString();
            }

            this.reservation_code = reservation_code;
        }
        internal void ReadFile()
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "raw", "reservationList.bin");
            try
            {
                string binaryData = File.ReadAllText(filepath);
                reservations.Clear();
                reservations = JsonSerializer.Deserialize<List<ReservationManagement>>(binaryData);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with binary file: {ex.Message}");
            }

        }
        
        internal void WriteReservation(ReservationManagement reservation)
        {
            bool alreadyTaken = false;
            do
            {
                foreach(ReservationManagement rm in reservations) 
                {
                    if (reservation.reservation_code == rm.reservation_code)
                    {
                        alreadyTaken = true;
                        reservation.CreateRC();
                        break;
                    }
                }
                alreadyTaken = false;
            }while (alreadyTaken);
            reservations.Add(reservation);

            string filepath= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "raw", "reservations.txt");

            try
            {
                string jsonString = JsonSerializer.Serialize(reservations);
                File.WriteAllText(filepath, jsonString);
                return;
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error reading Binary File: {e.Message}");
                return;
            }
        }
        internal List<ReservationManagement> SearchReservation(string reservation_code, string airline, string name)
        {
            reservation_code = reservation_code.ToUpper();
            airline = airline.ToUpper();
            name = name.ToUpper();
            List<ReservationManagement> matchingreservations = new List<ReservationManagement>();
            foreach(ReservationManagement reservation in reservations)
            {
                if(reservation.reservation_code.ToLower() == reservation_code||reservation_code=="ANY")
                {
                    if (reservation.airline.ToLower() == airline || airline == "ANY")
                    {
                        if (reservation.name.ToLower() == name || name == "any")
                        {
                            matchingreservations.Add(reservation);
                        }
                    }
                }
            }
            return matchingreservations;

        }

        internal void DeleteReservation(ReservationManagement reservation)
        {
            for(int i = 0;i<reservations.Count();i++) 
            {
                if (reservations[i].reservation_code==reservation.reservation_code)
                {
                    reservations.RemoveAt(i);
                    string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "raw", "reservations.txt");

                    try
                    {
                        string jsonString = JsonSerializer.Serialize(reservations);
                        File.WriteAllText(filepath, jsonString);
                        ReadFile();
                        return;
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine($"Error reading Binary File: {e.Message}");
                        return;
                    }
                }
            }
        }

        internal void ModifyReservation(ReservationManagement reservation)
        {
            for(int i = 0; i<reservations.Count();i++)
            {
                if (reservations[i].reservation_code == reservation.reservation_code) 
                {
                    reservations[i]= reservation;
                    string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "raw", "reservations.txt");

                    try
                    {
                        string jsonString = JsonSerializer.Serialize(reservations);
                        File.WriteAllText(filepath, jsonString);
                        ReadFile();
                        return;
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine($"Error reading Binary File: {e.Message}");
                        return;
                    }
                }
            }
            throw new System.Exception("No data is present");
        }
        public override string ToString()
        {
            return $"{reservation_code}: {flight_code}, {airline}, {cost}.00, {name}, {citizenship}";
        }
    }
}

