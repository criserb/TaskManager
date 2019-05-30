using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaskManager.Command;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    public class UpdateTaskViewModel : INotifyPropertyChanged
    {
        #region Properties
        private Task task;

        public Task Task
        {
            get { return task; }
            set { task = value; NotifyOnPropertyChanged("Task"); }
        }

        private TaskRepo _taskRepo;

        public TaskRepo TaskRepo
        {
            get { return _taskRepo; }
            set { _taskRepo = value; }
        }
        #endregion

        public UpdateTaskViewModel()
        {
            TaskRepo = new TaskRepo();
        }

        // Updating record
        #region Updating

        private ICommand _updateCommand;

        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new RelayCommand(SubmitUpdate, CanSubmitUpdate, false);
                }
                return _updateCommand;
            }
        }

        private bool CanSubmitUpdate(object parameter)
        {
            return true;
        }

        private void SubmitUpdate(object parameter)
        {
            TaskRepo.UpdateRecord(Task);
        }
        #endregion

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
