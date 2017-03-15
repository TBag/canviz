using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;

namespace CustomerApp
{
    public class ClaimViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }  
        }

        private string _status, _name, _index;
        private Color _bkcolor;
        public string Id { set; get; }

        public string Status
        {
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
            get
            {
                return _status;
            }
        }
        public string Name
        {
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
            get
            {
                return _name;
            }
        }
        public string Index
        {
            set
            {
                if (_index != value)
                {
                    _index = value;
                    Bkcolor =  (Int64.Parse(_index) % 2 == 1) ? Color.FromHex("#f3c86b") : Color.FromHex("#a0cc51");
                    OnPropertyChanged("Index");
                }
            }
            get
            {
                return _index;
            }
        }
        public Color Bkcolor
        {
            set
            {
                if (_bkcolor != value)
                {
                    _bkcolor = value;
                    OnPropertyChanged("Bkcolor");
                }
            }
            get
            {
                return _bkcolor;
            }
        }
    }
}
