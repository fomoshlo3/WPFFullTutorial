using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.ViewModels
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies the UI that a property has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets a calling property's value and invokes RaisePropertyChanged method. 
        /// </summary>
        /// <typeparam name="T">Proofs if field and newValue are of the same type</typeparam>
        /// <param name="field">backing field</param>
        /// <param name="newValue">new value for field</param>
        /// <param name="propertyName">default null, if not defined on call, the [CallerMemberName] flag inserts the name
        ///                            of the calling member</param>
        /// <returns>false if the values are equal, else true</returns>
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

        
    }
}
