using Microsoft.EntityFrameworkCore;
using Reservoom.DTOs;
using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.DbContexts
{
    public class ReservoomDbContext : DbContext
    {
        public DbSet<ReservationDTO> Reservations {  get; set; }

        public ReservoomDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
