using System;
using System.Windows.Input;

namespace Reservoom.ViewModels
{
    public class MakeReservationViewModel : ObservableObject, IDisposable
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        private int _roomNumber;

        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                RaisePropertyChanged(nameof(RoomNumber));
            }
        }

        private int _floorNumber;

        public int FloorNumber
        {
            get { return _floorNumber; }
            set
            {
                _floorNumber = value;
                RaisePropertyChanged(nameof(FloorNumber));
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                RaisePropertyChanged(nameof(EndDate));
            }
        }

        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
