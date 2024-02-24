using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;

namespace ZdravoCorp.HealthInstitution.GUI.MedicalTreatment.ViewModel
{
    public class PatientCareRoomListViewModel : INotifyPropertyChanged
    {
        public int RoomId { get; set; }
        public int NumberOfPatients { get; set; }
        public int MaxPatients { get; set; }
        public PatientCareRoomListViewModel(PatientCareRoomItem item)
        {
            RoomId = item.roomId;
            NumberOfPatients = item.numberOfPatients;
            MaxPatients = item.MaxPatients;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
