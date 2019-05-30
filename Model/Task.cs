using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class Task : INotifyPropertyChanged
    {
        #region Properties

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _priority;

        public string Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private string _term;

        public string Term
        {
            get { return _term; }
            set { _term = value; }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string s)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
            {
                ph(this, new PropertyChangedEventArgs(s));
            }
        }

        public string Show()
        {
            return $"Id: {Id} | Priorytet: {Priority} | Status: {Status} | Termin: {Term} | Treść: {Content}";
        }

        public void Clear()
        {
            //Priority = Priority.Normal;
            //Status = Status.New;
            Content = string.Empty;
            Priority = string.Empty;
            Status = string.Empty;
            Term = string.Empty;
        }
    }

    #region Enums

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Status
    {
        [Description("Nowy")]
        New,
        [Description("W realizacji")]
        InProgress,
        [Description("Zakończony")]
        End
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Priority
    {
        [Description("Niski")]
        Low,
        [Description("Normalny")]
        Normal,
        [Description("Wysoki")]
        High
    }

    #endregion
}
