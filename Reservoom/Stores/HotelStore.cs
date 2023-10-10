using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Stores
{
    public class HotelStore
    {
        private readonly Hotel _hotel;
        private readonly Lazy<Task> _initializeLazy;
        private readonly List<Reservation> _reservations;

        public IEnumerable<Reservation> Reservations => _reservations;

        public event Action<Reservation> ReservationMade;

        public HotelStore(Hotel hotel)
        {
            _reservations = new List<Reservation>();
            _initializeLazy = new(Initialize);
            _hotel = hotel;
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        void OnReservationMade(Reservation reservation)
        {
            ReservationMade?.Invoke(reservation);
        }

        public async Task MakeReservation(Reservation toMake)
        {
            await _hotel.MakeReservation(toMake);
            _reservations.Add(toMake);

            OnReservationMade(toMake);
        }

        private async Task Initialize()
        {
            var reservations = await _hotel.GetAllReservations();

            _reservations.Clear();
            _reservations.AddRange(reservations);
        }
    }
}
