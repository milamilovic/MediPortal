using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Model;

namespace ZdravoCorp.HealthInstitution.GUI.DaysOff.ViewModel
{
    public class DaysOffRequestListItemViewModel : INotifyPropertyChanged
    {
        internal DaysOffRequestListItemViewModel(DaysOffRequest dayOff)
        {
            RequestId = dayOff.RequestId;
            StartDate = dayOff.StartDate;
            EndDate = dayOff.EndDate;
            IsApproved = dayOff.Approved;
        }

        public int RequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
