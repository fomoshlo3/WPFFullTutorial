using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.Models;
using Reservoom.Services.ReservationCreators.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory=dbContextFactory;
        }

        private ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = reservation.RoomID?.FloorNumber ?? 0,
                RoomNumber = reservation.RoomID?.RoomNumber ?? 0,
                UserName = reservation.UserName,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
            };
        }

        public async Task CreateReservation(Reservation toCreate)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var reservationDTO = ToReservationDTO(toCreate);

                await dbContext.Reservations.AddAsync(reservationDTO);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
