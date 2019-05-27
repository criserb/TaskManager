using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    public class TheTask : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string s)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
            {
                ph(this, new PropertyChangedEventArgs(s));
            }
        }
    }
}
