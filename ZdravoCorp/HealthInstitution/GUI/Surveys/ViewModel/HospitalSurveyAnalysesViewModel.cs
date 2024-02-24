using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using ZdravoCorp.HealthInstitution.Core.Surveys.Commands;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.Core.Surveys.Repository;
using ZdravoCorp.HealthInstitution.DaysOff;
using ZdravoCorp.HealthInstitution.DaysOff.Repository;

namespace ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel
{
    public class HospitalSurveyAnalysesViewModel : INotifyPropertyChanged
    {


        private readonly ObservableCollection<HospitalAnalysisListViewModel> _surveyItems;
        public IEnumerable<HospitalAnalysisListViewModel> SurveyItems => _surveyItems;

        private readonly ObservableCollection<Comment> _comments;
        public IEnumerable<Comment> Comments => _comments;

        private readonly ISurveyRepository _repository;
        public LoadHospitalAnalysisCommand LoadCommand { get; }
        public HospitalSurveyAnalysesViewModel()
        {
            _repository = new SurveyRepository();
            _surveyItems = new ObservableCollection<HospitalAnalysisListViewModel>();
            _comments = new ObservableCollection<Comment>();
            LoadCommand = new LoadHospitalAnalysisCommand(this);
            InitializeItems();
        }

        private void InitializeItems()
        {
            _surveyItems.Clear();
            _comments.Clear();
            List<Survey> surveys = _repository.GetAll();
            if (surveys.Count == 0) return;
            List<double> averages = LoadCommand.GetAverages(ref surveys);
            for (int i = 0; i < surveys[0].Questions.Count; i++)
            {
                List<int> rates = LoadCommand.RatesForQuestionHospital(i, ref surveys);
                HospitalSurveyAnalysisItem item = new HospitalSurveyAnalysisItem(averages[i], rates[4], rates[3], rates[2], rates[1], rates[0], surveys[0].Questions[i]);
                _surveyItems.Add(new HospitalAnalysisListViewModel(item));
            }
            foreach (Survey survey in surveys)
            {
                if (survey.Comment != null)
                {
                    Comment comment = new Comment(survey.Comment);
                    _comments.Add(comment);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
