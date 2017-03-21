using System;
using System.ComponentModel;

namespace EmployeeApp
{
    public class ClaimStatus
    {
        public static string Approved= "Approved";
        public static string Pending = "Pending";
        public static string Declined = "Declined";
    }
    public class ClaimViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _status, _claimHint;
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

        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ClaimDateTime { get; set; }
        public string ClaimDescription { get; set; }

        private bool _isNew;
        public bool isNew
        {
            set
            {
                if (_isNew != value)
                {
                    _isNew = value;
                    OnPropertyChanged("isNew");
                }
            }
            get
            {
                return _isNew;
            }
        }
    }
}
