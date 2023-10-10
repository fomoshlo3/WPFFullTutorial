using Reservoom.Commands;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Reservoom.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        //private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        readonly HotelStore _hotelStore;



        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand LoadReservationsCommand { get; }
        public ICommand MakeReservationCommand { get; }

        public ReservationListingViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {
            _hotelStore = hotelStore;
            _reservations = new ObservableCollection<ReservationViewModel>();

            LoadReservationsCommand = new LoadReservationsCommand(this, hotelStore);
            MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

            _hotelStore.ReservationMade += OnReservationMade;
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel reservationListingViewModel = new(hotelStore, makeReservationNavigationService);
            reservationListingViewModel.LoadReservationsCommand.Execute(null);

            return reservationListingViewModel;
        }

        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMade;
            base.Dispose();
        }
        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach (var reservation in reservations)
            {
                var reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
        void OnReservationMade(Reservation reservation)
        {
            var reservationViewModel = new ReservationViewModel(reservation);

            _reservations.Add(reservationViewModel);
        }
    }
}
