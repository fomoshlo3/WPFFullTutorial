﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name { get; }
        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;
            _reservationBook = reservationBook;
        }

        /// <inheritdoc cref="ReservationBook.GetAllReservations"/>
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationBook.GetAllReservations();
        }

        /// <summary>Make a reservation.</summary>
        /// <inheritdoc cref="ReservationBook.AddReservation(Reservation)"/>x
        public async Task MakeReservation(Reservation reservationToMake)
        {
            await _reservationBook.AddReservation(reservationToMake);
        }
    }
}
