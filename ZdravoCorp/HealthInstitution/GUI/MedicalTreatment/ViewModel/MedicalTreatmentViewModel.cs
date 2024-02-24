using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Rooms.Commands;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using ZdravoCorp.HealthInstitution.Core.Rooms.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.Surveys;

namespace ZdravoCorp.HealthInstitution.GUI.MedicalTreatment.ViewModel
{
    public class MedicalTreatmentViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<PatientCareRoomListViewModel> _roomItems;
        public IEnumerable<PatientCareRoomListViewModel> RoomItems => _roomItems;

        public Patient SelectedPatient;
        public MedicalTreatmentReferral PatientsReferral;

        //private Room _selectedRoom { get; set; }
        public PatientCareRoomListViewModel SelectedRoom { get; set; } //{ get => _selectedRoom; set => _selectedRoom = value; }

        public PlacePatientInRoomCommand PlacePatientInRoomCommand { get; }

        public MedicalTreatmentViewModel(Patient patient, MedicalTreatmentReferral referral)
        {
            SelectedPatient = patient;
            PatientsReferral = referral;
            _roomItems = new ObservableCollection<PatientCareRoomListViewModel>();
            IntitalizeItems();
            PlacePatientInRoomCommand = new PlacePatientInRoomCommand(this);
        }

        public void IntitalizeItems()
        {
            _roomItems.Clear();
            List<Room> rooms = RoomSevice.GetAllRooms(false);
            foreach (Room room in rooms)
            {
                if (room.RoomType == Room.Type.PatientCareRoom && room.MaxPatientNumber > room.CurrentPatientNumber)
                {
                    PatientCareRoomItem item = new PatientCareRoomItem(room.Id, room.CurrentPatientNumber, room.MaxPatientNumber);
                    _roomItems.Add(new PatientCareRoomListViewModel(item));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
