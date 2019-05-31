using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Command;
using TaskManager.Model;
using TaskManager.Views;

namespace TaskManager.ViewModel
{
    class TaskViewModel : INotifyPropertyChanged
    {
        #region Properties

        private Visibility _tipVisibility;

        public Visibility TipVisibility
        {
            get { return _tipVisibility; }
            set { _tipVisibility = value; NotifyOnPropertyChanged("TipVisibility"); }
        }

        private TaskRepo _taskRepo;

        public TaskRepo TaskRepo
        {
            get { return _taskRepo; }
            set { _taskRepo = value; }
        }


        private Task _selectedTask;

        public Task SelectedTask
        {
            get { return _selectedTask; }
            set { _selectedTask = value; NotifyOnPropertyChanged("SelectedTask"); }
        }

        private Task _task;

        public Task Task
        {
            get { return _task; }
            set { _task = value; NotifyOnPropertyChanged("Task"); }
        }

        private ObservableCollection<Task> _tasks;

        public ObservableCollection<Task> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; NotifyOnPropertyChanged("Tasks"); }
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

        // Edit task window 
        private UpdateTaskViewModel _updateTaskViewModel;

        // Detail task window
        private DetailsTaskViewModel _detailsTaskViewModel;

        // Constructor
        public TaskViewModel()
        {
            // Initial view of updating task
            _updateTaskViewModel = new UpdateTaskViewModel();

            // Initial view of task details
            _detailsTaskViewModel = new DetailsTaskViewModel();

            // Initial selected task
            SelectedTask = new Task();

            // Initial task from database
            TaskRepo = new TaskRepo();

            // Observable collection of tasks to notify UI
            Tasks = new ObservableCollection<Task>(TaskRepo.TaskRepository);

            // Initial task with default values
            Task = new Task
            {
                Id = (Tasks.Count > 0) ? Tasks.Last().Id : 0
            };

            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        // Adding record Command
        #region Adding

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
            Task.Id++;
            Tasks.Add(Task);
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(Task.Content))
            {
                TipVisibility = Visibility.Visible;
                return false;
            }
            else
            {
                TipVisibility = Visibility.Hidden;
                return true;
            }
        }

        #endregion

        // Deleting record Command
        #region Deleting

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(SubmitDelete, CanSubmitDelete, false);
                }
                return _deleteCommand;
            }
        }

        private bool CanSubmitDelete(object parameter)
        {
            if (CheckSelectedTask())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SubmitDelete(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Na pewno chcesz usunąć ten rekord?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Tasks.Remove(SelectedTask);
            }

            // Initialize new SelectedTask object after delete record
            SelectedTask = new Task();
        }
        #endregion

        // Updating record Command
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
            if (CheckSelectedTask())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SubmitUpdate(object parameter)
        {
            UpdateTaskView view = new UpdateTaskView
            {
                DataContext = _updateTaskViewModel
            };

            _updateTaskViewModel.Task = SelectedTask;

            view.ShowDialog();

            RefreshCollection();
        }
        #endregion

        // Task Details Command
        #region ShowDetails

        private ICommand _detailsCommand;

        public ICommand DetailsCommand
        {
            get
            {
                if (_detailsCommand == null)
                {
                    _detailsCommand = new RelayCommand(SubmitDetails, CanSubmitDetails, false);
                }
                return _detailsCommand;
            }
        }

        private bool CanSubmitDetails(object parameter)
        {
            if (CheckSelectedTask())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SubmitDetails(object parameter)
        {
            DetailsTaskView view = new DetailsTaskView
            {
                DataContext = _detailsTaskViewModel
            };

            _detailsTaskViewModel.Task = SelectedTask;

            view.ShowDialog();
        }
        #endregion

        // Refresh list of tasks
        private void RefreshCollection()
        {
            Tasks = new ObservableCollection<Task>(TaskRepo.TaskRepository);
        }

        // Observer for changes of tasks observable collection
        private void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int newIndex = e.NewStartingIndex;
                TaskRepo.AddNewRecord(Tasks[newIndex]);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                List<Task> tempListOfRemovedItems = e.OldItems.OfType<Task>().ToList();
                TaskRepo.DelRecord(tempListOfRemovedItems[0].Id);
            }
        }

        private bool CheckSelectedTask()
        {
            if (SelectedTask.Id != 0)
                return true;
            else
                return false;
        }
    }
}
