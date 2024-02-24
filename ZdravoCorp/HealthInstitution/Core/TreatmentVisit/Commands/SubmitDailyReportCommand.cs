using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Repository;
using ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel;

namespace ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Commands
{
    public class SubmitDailyReportCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public NurseVisitViewModel viewModel;

        public SubmitDailyReportCommand(NurseVisitViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            MedicalTreatmentReferralRepository referralRepository = new MedicalTreatmentReferralRepository();
            MedicalTreatmentReferral referral = referralRepository.GetTreatmentById(viewModel.SelectedPatientOnTreatment.ReferralId);
            DailyReport report = new DailyReport(referral.PatientId, referral.TreatmentId, viewModel.HeartPressure, viewModel.Temperature, viewModel.Observations);
            DailyReportRepository repository = new DailyReportRepository();
            repository.Add(report);
            MessageBox.Show("Report has been stored.");
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
