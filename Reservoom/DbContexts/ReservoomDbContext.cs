using Microsoft.EntityFrameworkCore;
using Reservoom.DTOs;
using System;
using System.Linq;

namespace Reservoom.DbContexts
{
    public class ReservoomDbContext : DbContext
    {
        public DbSet<ReservationDTO> Reservations { get; set; }

        public ReservoomDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
