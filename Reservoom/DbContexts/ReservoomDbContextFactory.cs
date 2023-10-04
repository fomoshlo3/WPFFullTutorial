﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.DbContexts
{
    public class ReservoomDbContextFactory
    {
        private readonly string _connectionString;

        public ReservoomDbContextFactory(string connectionString)
        {
            _connectionString=connectionString;
        }

        public ReservoomDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new ReservoomDbContext(options);
        }
    }
}
