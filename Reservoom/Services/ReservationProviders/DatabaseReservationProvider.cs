using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.Models;
using Reservoom.Services.ReservationProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory=dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            try
            {
                using (var dbContext = _dbContextFactory.CreateDbContext())
                {
                    IEnumerable<ReservationDTO> reservationDTOs = await dbContext.Reservations.ToListAsync();

                    var reservations = reservationDTOs.Select(r => ToReservation(r)).ToList();

                    return reservations;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Reservation ToReservation(ReservationDTO Reservation)
        {
            return new Reservation(new RoomID(Reservation.FloorNumber, Reservation.RoomNumber), Reservation.UserName, Reservation.StartTime, Reservation.EndTime);
        }
    }
}
