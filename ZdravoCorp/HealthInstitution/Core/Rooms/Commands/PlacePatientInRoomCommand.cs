using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;
using ZdravoCorp.HealthInstitution.Core.Rooms.Services;
using ZdravoCorp.HealthInstitution.GUI.MedicalTreatment.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Commands
{
    public class PlacePatientInRoomCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public MedicalTreatmentViewModel viewModel;

        public PlacePatientInRoomCommand(MedicalTreatmentViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (viewModel.SelectedRoom != null)
            {
                MedicalTreatmentReferralRepository referralRepository = new MedicalTreatmentReferralRepository();
                MedicalTreatmentReferral[] referrals = referralRepository.LoadFile();
                foreach (MedicalTreatmentReferral referral in referrals)
                {
                    if (referral.TreatmentId == viewModel.PatientsReferral.TreatmentId)
                    {
                        referral.TreatmentStarted = true;
                        referral.RoomId = viewModel.SelectedRoom.RoomId;
                        referralRepository.UpdateTreatmentStarted(referral);
                    }
                }
                PatientCareRoomsServices.UpdateRooms();
                viewModel.IntitalizeItems();
                MessageBox.Show("Patient has been given the selected room.");
            }
            else
            {
                MessageBox.Show("Select a room.");
            }
        }
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
