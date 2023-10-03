using Reservoom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservoom.ViewModels
{
    public class ReservationListingViewModel : ObservableObject, IDisposable
    {
        private readonly ObservableCollection<Reservation> reservations;

        public ICommand MakeReservationCommand { get; }

        public ReservationListingViewModel()
        {
            
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
