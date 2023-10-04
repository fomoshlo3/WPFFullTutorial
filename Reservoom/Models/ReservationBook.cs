using Reservoom.Exceptions;
using Reservoom.Services.ReservationCreators.Contracts;
using Reservoom.Services.ReservationProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;



        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator)
        {
            _reservationProvider=reservationProvider;
            _reservationCreator=reservationCreator;
        }

        /// <summary>
        /// Gets all reservations.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of reservations.</returns>
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _reservationProvider.GetAllReservationsAsync();
        }

        /// <summary>
        /// Adds a reservation the reservation book.
        /// </summary>
        /// <param name="reservationToAdd">The incoming reservation.</param>
        /// <exception cref="ReservationConflictException">Thrown if incoming reservation conflicts with existing reservation.</exception>
        public async Task AddReservation(Reservation reservationToAdd)
        {
            foreach (var existingReservation in _reservations)
            {
                if (existingReservation.Conflicts(reservationToAdd))
                {
                    throw new ReservationConflictException(existingReservation, reservationToAdd);
                }
            }

            await _reservationCreator.CreateReservation(reservationToAdd);
        }
    }
}
