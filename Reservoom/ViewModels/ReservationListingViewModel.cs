using Reservoom.Commands;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservoom.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase, IDisposable
    {
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        private readonly Hotel _hotel;


        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand LoadReservationsCommand { get; }
        public ICommand MakeReservationCommand { get; }

        public ReservationListingViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
        {
            
            _reservations = new ObservableCollection<ReservationViewModel>();

            LoadReservationsCommand=new LoadReservationsCommand(hotel, this);
            MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

            
        }

        public static ReservationListingViewModel LoadViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel reservationListingViewModel = new(hotel, makeReservationNavigationService);
            reservationListingViewModel.LoadReservationsCommand.Execute(null);

            return reservationListingViewModel;
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach(var reservation in reservations)
            {
                var reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
