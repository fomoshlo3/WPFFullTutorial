using Reservoom.Exceptions;
using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Reservoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("Fomoshlo's Motel");


            try
            {
                hotel.MakeReservation
                (
                     new Reservation
                     (
                        new RoomID(1, 3),
                        "Fomo Shlo",
                        new DateTime(2023, 1, 1),
                        new DateTime(2023, 1, 2)
                     )
                );

                hotel.MakeReservation
                (
                    new Reservation
                    (
                        new RoomID(1, 1),
                        "Fomo Shlo",
                        new DateTime(2023, 1, 13),
                        new DateTime(2023, 1, 15)
                    )
                );

                hotel.MakeReservation
                (
                    new Reservation
                    (
                        new RoomID(1, 2),
                        "Fomo Shlo",
                        new DateTime(2023, 1, 1),
                        new DateTime(2023, 1, 5)
                    )
                );
            }
            catch (ReservationConflictException Rex)
            {

            }

            IEnumerable<Reservation> reservations = hotel.GetReservations();

            base.OnStartup(e);
        }
    }
}
