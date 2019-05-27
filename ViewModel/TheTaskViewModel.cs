using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Command;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    class TheTaskViewModel : INotifyPropertyChanged
    {
        private TheTask _theTask;

        public TheTask TheTask
        {
            get { return _theTask; }
            set { _theTask = value; NotifyOnPropertyChanged("TheTask"); }
        }

        private ObservableCollection<TheTask> _theTasks;

        public ObservableCollection<TheTask> TheTasks
        {
            get { return _theTasks; }
            set { _theTasks = value; NotifyOnPropertyChanged("TheTasks"); }
        }

        private void NotifyOnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public TheTaskViewModel()
        {
            TheTasks = new ObservableCollection<TheTask>();

            TheTask = new TheTask();

            TheTasks.Add(new TheTask
            {
                Id = 1,
                Content = "dasda",
                Priority = "normalny",
                Status = "dasd"
            });
        }

        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new RelayCommand(SubmitExecute, CanSubmitExecute, false);
                }
                return _submitCommand;
            }
        }

        private void SubmitExecute(object parameter)
        {
            TheTask.Id = TheTasks.Count + 1;
            TheTasks.Add(TheTask);
        }

        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
