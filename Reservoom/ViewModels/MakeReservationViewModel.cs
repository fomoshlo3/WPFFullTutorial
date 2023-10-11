using Reservoom.Commands;
using Reservoom.Services;
using Reservoom.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Reservoom.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
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
            set
            {
                Set(ref _startDate, value);

                ClearErrors(nameof(EndDate));
                ClearErrors(nameof(StartDate));
                if (EndDate < StartDate)
                {
                    AddError("The start date cannot be after the end date.", nameof(StartDate));
                }
            }
        }

        private DateTime _endDate = new DateTime(2023, 1, 1);


        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                Set(ref _endDate, value);

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("The end date cannot be before start date.", nameof(EndDate));
                }
            }
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }


        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;

        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ReservationListingViewModel> reservationListingViewNavigationsService)
        {
            SubmitCommand = new SubmitReservationCommand(this, hotelStore, reservationListingViewNavigationsService);
            CancelCommand = new NavigateCommand<ReservationListingViewModel>(reservationListingViewNavigationsService);
            _propertyNameToErrorsDictionary = new();
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
