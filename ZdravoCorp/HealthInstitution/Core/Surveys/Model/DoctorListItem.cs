using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Model
{
    public class DoctorListItem : INotifyPropertyChanged
    {
        public DoctorListItem(int docId, double docRate)
        {
            DoctorIdentification = docId;
            DoctorRate = docRate;
        }

        public int DoctorIdentification { get; set; }
        public double DoctorRate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
