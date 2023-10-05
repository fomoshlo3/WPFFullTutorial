using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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

        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var reservationDTO = await (from entry in dbContext.Reservations
                                           where entry.FloorNumber == reservation.RoomID.FloorNumber
                                           where entry.RoomNumber == reservation.RoomID.RoomNumber
                                           where entry.EndTime > reservation.StartTime
                                           where entry.StartTime < reservation.EndTime
                                           select new ReservationDTO
                                           {
                                               Id = entry.Id,
                                               UserName = entry.UserName,
                                               FloorNumber = entry.FloorNumber,
                                               RoomNumber = entry.RoomNumber,
                                               EndTime = entry.EndTime,
                                               StartTime = entry.StartTime
                                           }).FirstOrDefaultAsync();
                                                       
                                            

               //var reservationDTO = await dbContext.Reservations
               //     .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
               //     .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
               //     .Where(r => r.EndTime > reservation.StartTime)
               //     .Where(r => r.StartTime < reservation.EndTime)
               //     .FirstOrDefaultAsync();

                if (reservationDTO == null)
                {
                    return null;
                }

                return ToReservation(reservationDTO);
            }
        }

        private static Reservation ToReservation(ReservationDTO reservationDTO)
        {
            return new Reservation(new RoomID(reservationDTO.FloorNumber, reservationDTO.RoomNumber), reservationDTO.UserName, reservationDTO.StartTime, reservationDTO.EndTime);
        }
    }
}
