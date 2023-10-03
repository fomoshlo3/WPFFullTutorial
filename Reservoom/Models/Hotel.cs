using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Reservoom.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name { get; }
        public Hotel(string name)
        {
            Name = name;
            _reservationBook = new ReservationBook();
        }

        /// <inheritdoc cref="ReservationBook.GetReservations"/>
        public IEnumerable<Reservation> GetReservations()
        {
            return _reservationBook.GetReservations();
        }

        /// <summary>Make a reservation.</summary>
        /// <inheritdoc cref="ReservationBook.AddReservation(Reservation)"/>x
        public void MakeReservation(Reservation reservationToMake)
        {
            _reservationBook.AddReservation(reservationToMake);
        }
    }
}
