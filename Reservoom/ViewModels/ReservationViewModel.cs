﻿using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.ViewModels
{
    public class ReservationViewModel : ObservableObject, IDisposable
    {
        readonly Reservation reservation;

        public string RoomID => reservation.RoomID?.ToString();
        public  string UserName => reservation.UserName;
        public DateTime StartTime => reservation.StartTime;
        public DateTime EndTime => reservation.EndTime;

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

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
