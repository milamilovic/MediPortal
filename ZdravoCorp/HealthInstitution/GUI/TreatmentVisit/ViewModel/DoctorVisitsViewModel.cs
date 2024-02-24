using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Commands;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Services;

namespace ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel
{
    public class DoctorVisitsViewModel : INotifyPropertyChanged
    {
        public DoctorVisitsService VisitsService;
        public int DocId { get; set; }
        public string NewTherapy { get; set; }
        public DateTime NewDate { get; set; }

        private readonly ObservableCollection<DoctorVisitsGridViewModel> _treatments;
        public IEnumerable<DoctorVisitsGridViewModel> Treatments => _treatments;
        private DoctorVisitsGridViewModel _selectedTreatment { get; set; }
        public DoctorVisitsGridViewModel SelectedTreatment { get => _selectedTreatment; set => _selectedTreatment = value; }

        public ChangeTreatmentTherapyCommand ChangeTreatmentTherapyCommand { get; }
        public ChangeTreatmentDateCommand ChangeTreatmentDateCommand { get; }
        public EndTreatmentCommand EndTreatmentCommand { get; }


        private int _selected;
        public int Selected
        {
            get => _selected;
            set
            {
                if (value < 0) { return; }
                _selected = value;
                OnPropertyChanged(nameof(Selected));
                _selectedTreatment = _treatments.ElementAt(_selected);
            }
        }
        public DoctorVisitsViewModel(int docId)
        {
            DocId = docId;
            NewTherapy = "";
            NewDate = DateTime.Now;
            _treatments = new ObservableCollection<DoctorVisitsGridViewModel>();
            MedicalTreatmentReferralRepository treatmentRepository = new MedicalTreatmentReferralRepository();
            VisitsService = new DoctorVisitsService(treatmentRepository);
            InitializeTreatments();
            ChangeTreatmentTherapyCommand = new ChangeTreatmentTherapyCommand(this);
            ChangeTreatmentDateCommand = new ChangeTreatmentDateCommand(this);
            EndTreatmentCommand = new EndTreatmentCommand(this);

        }
        public MedicalTreatmentReferral ConvertToTreatment()
        {
            MedicalTreatmentReferral treatment = new MedicalTreatmentReferral
                  (SelectedTreatment.PatientId, SelectedTreatment.DoctorId,
                  SelectedTreatment.Days, SelectedTreatment.Therapy,
                  SelectedTreatment.AdditionalExams, SelectedTreatment.StartDate,
                  SelectedTreatment.EndDate);
            return treatment;
        }

        public void InitializeTreatments()
        {
            _treatments.Clear();
            List<MedicalTreatmentReferral> treatments = VisitsService.LoadCurrentTreatments(DocId);
            foreach (MedicalTreatmentReferral treatment in treatments)
            {

                _treatments.Add(new DoctorVisitsGridViewModel(treatment));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
