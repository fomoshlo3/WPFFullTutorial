using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System.Windows;

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

        public App()
        {
            _hotel = new Hotel("SingletonSean Suite");
            _navigationStore= new();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            /* Note: SqlLite specific solution, since a sqlite db will be created in the root of the folder, 
             * when setting the connection string with Data Source=xxxx.db which leads to the app not finding it in the bin/Debug.
             * it is best to instantiate the referenciing DbContext class in the overridden startup method 
             */
            var options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (var dbContext = new ReservoomDbContext(options))
            {
                dbContext.Database.EnsureCreated();
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
            return new ReservationListingViewModel(_hotel, new NavigationService(_navigationStore, Create_MakeReservationViewModel));
        }
        private MakeReservationViewModel Create_MakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, Create_ReservationListingViewModel));
        }
    }
}
