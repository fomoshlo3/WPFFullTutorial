using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Services.ReservationConflictValidators.Contracts
{
    public interface IReservationConflictValidator
    {
        Task<Reservation> GetConflictingReservation(Reservation reservation);
    }
}
