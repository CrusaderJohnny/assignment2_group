using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Traveless.Services
{
    public class ReservationsManager
    {
        internal List<Reservations>? Reservations = new List<Reservations>();

        internal void SaveToFile(Reservations reservations)
        {
            bool exists = false;
            do
            {
                foreach (Reservations reserv in Reservations)
                {
                    if(reservations.Reservation_Code == reserv.Reservation_Code)
                    {
                        exists = true;
                        reservations.RandomReservationCode();
                        break;
                    }
                }
                exists = false;
            }while (exists);
            Reservations.Add(reservations);
            string saveFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "reservations", "reservations.txt");
            try
            {
                string jstring = JsonSerializer.Serialize(Reservations);
                File.WriteAllText(saveFile, jstring);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return;
            }
        }
        internal void UpdateReservationFile(Reservations reservations)
        {
            for (int i = 0; i < Reservations.Count; i++)
            {
                if (Reservations[i].Reservation_Code == reservations.Reservation_Code)
                {
                    Reservations[i] = reservations;
                    string saveFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "reservations", "reservations.txt");
                    try
                    {
                        string jstring = JsonSerializer.Serialize(Reservations);
                        File.WriteAllText(saveFile, jstring);
                        LoadReservations();
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading file: {ex.Message}");
                        return;
                    }
                }
            }
            throw new Exception("No matching entry found");
        }
        internal void RemoveReservationFile(Reservations reservations)
        {
            for(int i = 0;i < Reservations.Count;i++)
            {
                if (Reservations[i].Reservation_Code == reservations.Reservation_Code)
                {
                    Reservations.RemoveAt(i);
                    string saveFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "reservations", "reservations.txt");
                    try
                    {
                        string jstring = JsonSerializer.Serialize(Reservations);
                        File.WriteAllText(saveFile, jstring);
                        LoadReservations();
                        return;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error reading file: {ex.Message}");
                        return;
                    }
                }
            }
            throw new Exception("No matching entry found");
        }
        internal void LoadReservations()
        {
            string saveFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "reservations", "reservations.txt");
            try
            {
                string jstring = File.ReadAllText(saveFile);
                Reservations.Clear();
                Reservations = JsonSerializer.Deserialize<List<Reservations>>(jstring);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return;
            }
        }
        internal List<Reservations> SearchReservations(string reservation_code, string flightName, string clientName)
        {
            reservation_code = reservation_code.ToUpper();
            flightName = flightName.ToLower();
            clientName = clientName.ToLower();
            List<Reservations> foundReservations = new List<Reservations>();
            foreach(Reservations reservation in Reservations)
            {
                if(reservation.Reservation_Code == reservation_code || reservation_code == "ANY")
                {
                    if(reservation.FlightName.ToLower() == flightName || flightName == "any")
                    {
                        if(reservation.ClientName.ToLower() == clientName || clientName == "any")
                        {
                            foundReservations.Add(reservation);
                        } 
                    }
                }
            }
            return foundReservations;
        }
        internal Reservations SearchReservations(string reservation_code)
        {
            reservation_code = reservation_code.ToUpper();
            foreach(Reservations reservation in Reservations)
            {
                if(reservation.Reservation_Code == reservation_code)
                {
                    return reservation;
                }
            }
            return new Reservations();
        }
    }
}
