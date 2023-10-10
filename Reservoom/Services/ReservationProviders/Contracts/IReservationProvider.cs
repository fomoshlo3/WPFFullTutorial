﻿using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservoom.Services.ReservationProviders.Contracts
{
    public interface IReservationProvider
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    }
}
