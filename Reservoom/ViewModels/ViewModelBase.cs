using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Reservoom.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies the UI that a property has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets a calling property's value and invokes OnPropertyChanged method. 
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
            OnPropertyChanged(propertyName);
            return true;
        }

        public virtual void Dispose() { }
    }
}
