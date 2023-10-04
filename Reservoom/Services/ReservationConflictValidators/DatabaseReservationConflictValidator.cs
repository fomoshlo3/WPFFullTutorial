using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.Models;
using Reservoom.Services.ReservationConflictValidators.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Services.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory=dbContextFactory;
        }

        public async Task<bool> DoesCauseConflict(Reservation reservation)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
               return await dbContext.Reservations.Select(r => ToReservation(r)).AnyAsync(r => r.Conflicts(reservation));
            }
        }

        private static Reservation ToReservation(ReservationDTO r)
        {
            return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber), r.UserName, r.StartTime, r.EndTime);
        }
    }
}
