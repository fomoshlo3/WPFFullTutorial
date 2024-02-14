﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservoom.DbContexts;
using Reservoom.HostBuilder;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Services.ReservationConflictValidators;
using Reservoom.Services.ReservationConflictValidators.Contracts;
using Reservoom.Services.ReservationCreators;
using Reservoom.Services.ReservationCreators.Contracts;
using Reservoom.Services.ReservationProviders;
using Reservoom.Services.ReservationProviders.Contracts;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System;
using System.Windows;

namespace Reservoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        //private readonly Hotel _hotel;
        //private readonly NavigationStore _navigationStore;
        //private readonly HotelStore _hotelStore;
        //private readonly ReservoomDbContextFactory _reservoomDbContextFactory;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {
                    string connString = hostContext.Configuration.GetConnectionString("Default");
                    var hotels = hostContext.Configuration.GetSection("Hotels");
                    
                    services.AddSingleton(new ReservoomDbContextFactory(connString))
                            .AddSingleton<IReservationProvider, DatabaseReservationProvider>()
                            .AddSingleton<IReservationCreator, DatabaseReservationCreator>()
                            .AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>()

                            .AddTransient<ReservationBook>()
                            .AddSingleton((s) => new Hotel(hotels["Default"], s.GetRequiredService<ReservationBook>()))

                            .AddSingleton<HotelStore>()
                            .AddSingleton<NavigationStore>()

                            .AddSingleton(s => new MainWindow()
                            {
                                DataContext = s.GetRequiredService<MainViewModel>()
                            });
                })
                .Build();

            //_reservoomDbContextFactory = new(CONNECTION_STRING);
            //IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservoomDbContextFactory);
            //IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContextFactory);
            //IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservoomDbContextFactory);
            //ReservationBook reservationBook = new(reservationProvider, reservationCreator, reservationConflictValidator);

            //_hotel = new Hotel("SingletonSean Suite", reservationBook);
            //_hotelStore = new HotelStore(_hotel);q
            //_navigationStore= new();
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host.Start();

            var ctxFactory = _host.Services.GetRequiredService<ReservoomDbContextFactory>();
            using (var dbContext = ctxFactory.CreateDbContext())
            {
                /* Note: SqlLite specific solution, since a sqlite db will be created in the root of the folder, 
                 * when setting the connection string with Data Source=xxxx.db which leads to the app not finding it in the bin/Debug.
                 * it is best to instantiate the referenciing DbContext class in the overridden startup method 
                 */
                dbContext.Database.Migrate();
            };

            var navService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

        }

        //private MakeReservationViewModel Create_MakeReservationViewModel()
        //{
        //    return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, Create_ReservationListingViewModel));
        //}
        //private ReservationListingViewModel Create_ReservationListingViewModel()
        //{
        //    return ReservationListingViewModel.LoadViewModel(_hotelStore, new NavigationService(_navigationStore, Create_MakeReservationViewModel));
        //}

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            _host.Dispose();
        }
    }
}
