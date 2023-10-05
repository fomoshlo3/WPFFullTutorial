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
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = await dbContext.Reservations.ToListAsync();

                return reservationDTOs.Select(r => ToReservation(r));
            }
        }

        private static Reservation ToReservation(ReservationDTO r)
        {
            return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber), r.UserName, r.StartTime, r.EndTime);
        }
    }
}
