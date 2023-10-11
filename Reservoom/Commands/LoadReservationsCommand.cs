using Reservoom.Models;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
    
        private readonly ReservationListingViewModel _reservationListingViewModel;
        private readonly HotelStore _hotelStore;

        public LoadReservationsCommand(ReservationListingViewModel reservationListingViewModel, HotelStore hotelStore)
        {
            _reservationListingViewModel = reservationListingViewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _reservationListingViewModel.ErrorMessage = string.Empty;
            _reservationListingViewModel.IsLoading = true;

            try
            {
                await _hotelStore.Load();

                _reservationListingViewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                _reservationListingViewModel.ErrorMessage = "Failed to load reservations.";
            }
            
            //surround this with a finally{} if ExecuteAsync() could throw errors
            _reservationListingViewModel.IsLoading = false;
            
        }
    }
}
