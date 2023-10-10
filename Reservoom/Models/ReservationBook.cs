﻿using Reservoom.Exceptions;
using Reservoom.Services.ReservationConflictValidators.Contracts;
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
        private readonly IReservationConflictValidator _reservationConflictValidator;



        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider=reservationProvider;
            _reservationCreator=reservationCreator;
            _reservationConflictValidator=reservationConflictValidator;
        }

        /// <summary>
        /// Gets all reservations.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of reservations.</returns>
        public async Task<IEnumerable<Reservation>> GetAllReservations()
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
            var conflictingReservation = await _reservationConflictValidator.GetConflictingReservation(reservationToAdd);

            if (conflictingReservation != null)
            {
                throw new ReservationConflictException(conflictingReservation, reservationToAdd);
            }

            await _reservationCreator.CreateReservation(reservationToAdd);
        }
    }
}
