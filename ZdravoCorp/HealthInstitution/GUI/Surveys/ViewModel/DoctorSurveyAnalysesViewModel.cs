using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Surveys;
using ZdravoCorp.HealthInstitution.Core.Surveys.Commands;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.Core.Surveys.Repository;

namespace ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel
{
    public class DoctorSurveyAnalysesViewModel : INotifyPropertyChanged
    {

        private readonly ObservableCollection<DoctorAnalysisListViewModel> _surveyItems;
        public IEnumerable<DoctorAnalysisListViewModel> SurveyItems => _surveyItems;

        private readonly ObservableCollection<DoctorListItem> _bestDoctors;
        public IEnumerable<DoctorListItem> BestDoctors => _bestDoctors;

        private readonly ObservableCollection<DoctorListItem> _worstDoctors;
        public IEnumerable<DoctorListItem> WorstDoctors => _worstDoctors;

        public LoadDoctorAnalysisCommand LoadCommand { get; }
        public DoctorSurveyAnalysesViewModel()
        {
            LoadCommand = new LoadDoctorAnalysisCommand(this);
            _surveyItems = new ObservableCollection<DoctorAnalysisListViewModel>();
            _bestDoctors = new ObservableCollection<DoctorListItem>();
            _worstDoctors = new ObservableCollection<DoctorListItem>();
            InitializeItems();
        }

        private void InitializeItems()
        {
            _surveyItems.Clear();
            _bestDoctors.Clear();
            _worstDoctors.Clear();

            DoctorSurveyRepository doctorSurveyRepository = new DoctorSurveyRepository();
            List<DoctorSurvey> surveys = doctorSurveyRepository.GetAll();
            if (surveys.Count == 0)
                return;

            List<int> doctorIds = LoadCommand.GetDoctors(ref surveys);
            List<DoctorListItem> doctors = GetDoctorListItems(surveys, doctorIds);
            SortDoctorsByRate(doctors);
            SetBestDoctors(doctors);
            SetWorstDoctors(doctors);
        }

        private List<DoctorListItem> GetDoctorListItems(List<DoctorSurvey> surveys, List<int> doctorIds)
        {
            List<DoctorListItem> doctors = new List<DoctorListItem>();

            foreach (int doctorId in doctorIds)
            {
                double average = 0;
                int count = 0;
                for (int i = 0; i < surveys[0].Questions.Count; i++)
                {
                    List<int> rates = LoadCommand.RatesForQuestionDoctor(ref surveys, doctorId, i);
                    double itemAverage = 0;
                    itemAverage = 5 * rates[4] + 4 * rates[3] + 3 * rates[2] + 2 * rates[1] + rates[0];
                    itemAverage = itemAverage / (rates[0] + rates[1] + rates[2] + rates[3] + rates[4]);
                    average += itemAverage;
                    count++;
                    DoctorSurveyAnalysisItem item = new DoctorSurveyAnalysisItem(itemAverage, rates[4], rates[3], rates[2], rates[1], rates[0], surveys[0].Questions[i], doctorId);
                    _surveyItems.Add(new DoctorAnalysisListViewModel(item));

                }
                average = average / count;
                DoctorListItem doctor = new DoctorListItem(doctorId, average);
                doctors.Add(doctor);
            }

            return doctors;
        }

        private void SortDoctorsByRate(List<DoctorListItem> doctors)
        {
            doctors.Sort((x, y) => y.DoctorRate.CompareTo(x.DoctorRate));
        }

        private void SetBestDoctors(List<DoctorListItem> doctors)
        {
            List<DoctorListItem> bestDoctors = doctors.Take(3).ToList();

            foreach (DoctorListItem bestDoctor in bestDoctors)
            {
                _bestDoctors.Add(bestDoctor);
            }
        }

        private void SetWorstDoctors(List<DoctorListItem> doctors)
        {
            doctors.Sort((x, y) => x.DoctorRate.CompareTo(y.DoctorRate));

            List<DoctorListItem> worstDoctors = doctors.Take(3).ToList();

            foreach (DoctorListItem worstDoctor in worstDoctors)
            {
                _worstDoctors.Add(worstDoctor);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
