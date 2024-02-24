using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Commands;

namespace ZdravoCorp.HealthInstitution.GUI.DaysOff.ViewModel
{
    internal class DoctorDaysOffViewModel : INotifyPropertyChanged
    {
        public int DocId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }

        public SendDaysOffRequestCommand SendDaysOffRequestCommand { get; }

        public DoctorDaysOffViewModel(int docId)
        {
            DocId = docId;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Reason = "";
            SendDaysOffRequestCommand = new SendDaysOffRequestCommand(this);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
