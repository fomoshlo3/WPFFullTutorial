using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Services.ReservationCreators.Contracts;
using Reservoom.Services.ReservationProviders.Contracts;
using Reservoom.Services.ReservationConflictValidators.Contracts;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System.Windows;
using Reservoom.Services.ReservationProviders;
using Reservoom.Services.ReservationCreators;
using Reservoom.Services.ReservationConflictValidators;

namespace Reservoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=reservoom.db";
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private readonly ReservoomDbContextFactory _reservoomDbContextFactory;

        public App()
        {
            _reservoomDbContextFactory = new(CONNECTION_STRING);
            IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservoomDbContextFactory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservoomDbContextFactory);
            ReservationBook reservationBook = new(reservationProvider, reservationCreator, reservationConflictValidator);

            _hotel = new Hotel("SingletonSean Suite", reservationBook);
            _navigationStore= new();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            /* Note: SqlLite specific solution, since a sqlite db will be created in the root of the folder, 
             * when setting the connection string with Data Source=xxxx.db which leads to the app not finding it in the bin/Debug.
             * it is best to instantiate the referenciing DbContext class in the overridden startup method 
             */
            using (var dbContext = _reservoomDbContextFactory.CreateDbContext())
            {
               dbContext.Database.Migrate();
            };


            _navigationStore.CurrentViewModel = Create_ReservationListingViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private ReservationListingViewModel Create_ReservationListingViewModel()
        {
            return ReservationListingViewModel.LoadViewModel(_hotel, new NavigationService(_navigationStore, Create_MakeReservationViewModel));
        }
        private MakeReservationViewModel Create_MakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, Create_ReservationListingViewModel));
        }
    }
}
