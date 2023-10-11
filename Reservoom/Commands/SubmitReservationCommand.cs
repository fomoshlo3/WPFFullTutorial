using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using Reservoom.Services;
using System.Threading.Tasks;
using Reservoom.Stores;

namespace Reservoom.Commands
{
    public class SubmitReservationCommand : AsyncCommandBase
    {
        private readonly Hotel _hotel;
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationService<ReservationListingViewModel> _reservationListingViewNavigationService;

        public SubmitReservationCommand(MakeReservationViewModel makeReservationViewModel,
                                        HotelStore hotelStore, 
                                        NavigationService<ReservationListingViewModel> reservationListingViewNavigationService)
        {
            _makeReservationViewModel=makeReservationViewModel;
            _hotelStore = hotelStore;
            _reservationListingViewNavigationService = reservationListingViewNavigationService;
            

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_makeReservationViewModel.UserName) &&
                _makeReservationViewModel.FloorNumber > 0 &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var reservation = new Reservation(
                new RoomID(
                    _makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber
                    ),
                _makeReservationViewModel.UserName,
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate);

            try
            {
                await _hotelStore.MakeReservation(reservation);

                MessageBox.Show("Successfully booked reservation.","Success",MessageBoxButton.OK, MessageBoxImage.Information);

                _reservationListingViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to make reservation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_makeReservationViewModel.UserName) || e.PropertyName == nameof(_makeReservationViewModel.FloorNumber))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
