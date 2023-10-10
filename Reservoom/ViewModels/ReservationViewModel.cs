using Reservoom.Models;
using System;
using System.Linq;

namespace Reservoom.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        readonly Reservation reservation;

        public string RoomID => reservation.RoomID?.ToString();
        public string UserName => reservation.UserName;
        public string StartTime => reservation.StartTime.ToString("d");
        public string EndTime => reservation.EndTime.ToString("d");

        //private string _roomID;

        //public string RoomID
        //{
        //    get { return _roomID; }
        //    set 
        //    {
        //        _roomID = value;
        //        RaisePropertyChanged(nameof(RoomID));
        //    }
        //}

        //private string _userName;

        //public string UserName
        //{
        //    get { return _userName; }
        //    set 
        //    {
        //        _userName = value;
        //        RaisePropertyChanged(nameof(UserName));
        //    }
        //}

        //private DateTime _startDate;

        //public DateTime StartDate
        //{
        //    get { return _startDate; }
        //    set
        //    {
        //        _startDate = value;
        //        RaisePropertyChanged(nameof(StartDate));
        //    }
        //}

        //private DateTime _endDate;

        //public DateTime EndDate
        //{
        //    get { return _endDate; }
        //    set
        //    {
        //        _endDate = value;
        //        RaisePropertyChanged(nameof(EndDate));
        //    }
        //}




        public ReservationViewModel(Reservation reservation)
        {
            this.reservation=reservation;
        }

    }
}
