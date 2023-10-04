﻿using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using Reservoom.Services;

namespace Reservoom.Commands
{
    public class SubmitReservationCommand : CommandBase
    {
        private readonly Hotel _hotel;
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly NavigationService _reservationListingViewNavigationService;

        public SubmitReservationCommand(MakeReservationViewModel makeReservationViewModel, Hotel hotel, NavigationService reservationListingViewNavigationService)
        {
            _makeReservationViewModel=makeReservationViewModel;
            _reservationListingViewNavigationService = reservationListingViewNavigationService;
            _hotel = hotel;

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_makeReservationViewModel.UserName) &&
                _makeReservationViewModel.FloorNumber > 0 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
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
                _hotel.MakeReservation(reservation);
                MessageBox.Show("Successfully booked reservation.","Success",MessageBoxButton.OK, MessageBoxImage.Information);

                _reservationListingViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken","Error",MessageBoxButton.OK, MessageBoxImage.Error);
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