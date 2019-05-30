using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    class DetailsTaskViewModel : INotifyPropertyChanged
    {
        public Task Task { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyOnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        } 
    }
}
