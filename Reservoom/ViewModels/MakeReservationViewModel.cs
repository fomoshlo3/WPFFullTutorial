using Reservoom.Commands;
using Reservoom.Services;
using Reservoom.Stores;
using System;
using System.Windows.Input;

namespace Reservoom.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        private int _roomNumber;

        public int RoomNumber
        {
            get => _roomNumber;
            set => Set(ref _roomNumber, value);
        }

        private int _floorNumber;

        public int FloorNumber
        {
            get => _floorNumber;
            set => Set(ref _floorNumber, value);
        }

        private DateTime _startDate = new DateTime(2023, 1, 1);

        public DateTime StartDate
        {
            get => _startDate;
            set => Set(ref _startDate, value);
        }

        private DateTime _endDate = new DateTime(2023, 1, 1);

        public DateTime EndDate
        {
            get => _endDate;
            set => Set(ref _endDate, value);
        }

        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationListingViewNavigationsService)
        {
            SubmitCommand = new SubmitReservationCommand(this, hotelStore, reservationListingViewNavigationsService);
            CancelCommand = new NavigateCommand(reservationListingViewNavigationsService);
        }
    }
}
