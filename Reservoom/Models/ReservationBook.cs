using Reservoom.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations;

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        /// <summary>
        /// Gets all reservations.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of reservations.</returns>
        public IEnumerable<Reservation> GetReservations()
        {
            return _reservations;
        }

        /// <summary>
        /// Adds a reservation the reservation book.
        /// </summary>
        /// <param name="reservationToAdd">The incoming reservation</param>
        /// <exception id="1" cref="ReservationConflictException"></exception>
        public void AddReservation(Reservation reservationToAdd)
        {
            foreach (var existingReservation in _reservations)
            {
                if (existingReservation.Conflicts(reservationToAdd))
                {
                    throw new ReservationConflictException(existingReservation, reservationToAdd);
                }
            }
            _reservations.Add(reservationToAdd);
        }
    }
}
