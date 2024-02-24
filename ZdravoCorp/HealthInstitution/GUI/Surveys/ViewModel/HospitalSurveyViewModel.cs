using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZdravoCorp.HealthInstitution.Core.Surveys.Commands;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.Core.Surveys.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel
{
    public class HospitalSurveyViewModel : INotifyPropertyChanged
    {
        public Patient Patient;
        private string _question1;
        public string Question1
        {
            get { return _question1; }
            set
            {
                if (_question1 != value)
                {
                    _question1 = value;
                    OnPropertyChanged(nameof(Question1));
                }
            }
        }
        private string _question2;
        public string Question2
        {
            get { return _question2; }
            set
            {
                if (_question2 != value)
                {
                    _question2 = value;
                    OnPropertyChanged(nameof(Question2));
                }
            }
        }
        private string _question3;
        public string Question3
        {
            get { return _question3; }
            set
            {
                if (_question3 != value)
                {
                    _question3 = value;
                    OnPropertyChanged(nameof(Question3));
                }
            }
        }
        private string _question4;
        public string Question4
        {
            get { return _question4; }
            set
            {
                if (_question4 != value)
                {
                    _question4 = value;
                    OnPropertyChanged(nameof(Question4));
                }
            }
        }
        private bool _isCheckedQ1R1;
        public bool IsCheckedQ1R1
        {
            get { return _isCheckedQ1R1; }
            set
            {
                _isCheckedQ1R1 = value;
                OnPropertyChanged(nameof(IsCheckedQ1R1));
            }
        }
        private bool _isCheckedQ1R2;
        public bool IsCheckedQ1R2
        {
            get { return _isCheckedQ1R2; }
            set
            {
                _isCheckedQ1R2 = value;
                OnPropertyChanged(nameof(IsCheckedQ1R2));
            }
        }
        private bool _isCheckedQ1R3;
        public bool IsCheckedQ1R3
        {
            get { return _isCheckedQ1R3; }
            set
            {
                _isCheckedQ1R3 = value;
                OnPropertyChanged(nameof(IsCheckedQ1R3));
            }
        }
        private bool _isCheckedQ1R4;
        public bool IsCheckedQ1R4
        {
            get { return _isCheckedQ1R4; }
            set
            {
                _isCheckedQ1R4 = value;
                OnPropertyChanged(nameof(IsCheckedQ1R4));
            }
        }
        private bool _isCheckedQ1R5;
        public bool IsCheckedQ1R5
        {
            get { return _isCheckedQ1R5; }
            set
            {
                _isCheckedQ1R5 = value;
                OnPropertyChanged(nameof(IsCheckedQ1R5));
            }
        }
        private bool _isCheckedQ2R1;
        public bool IsCheckedQ2R1
        {
            get { return _isCheckedQ2R1; }
            set
            {
                _isCheckedQ2R1 = value;
                OnPropertyChanged(nameof(IsCheckedQ2R1));
            }
        }
        private bool _isCheckedQ2R2;
        public bool IsCheckedQ2R2
        {
            get { return _isCheckedQ2R2; }
            set
            {
                _isCheckedQ2R2 = value;
                OnPropertyChanged(nameof(IsCheckedQ2R2));
            }
        }
        private bool _isCheckedQ2R3;
        public bool IsCheckedQ2R3
        {
            get { return _isCheckedQ2R3; }
            set
            {
                _isCheckedQ2R3 = value;
                OnPropertyChanged(nameof(IsCheckedQ2R3));
            }
        }
        private bool _isCheckedQ2R4;
        public bool IsCheckedQ2R4
        {
            get { return _isCheckedQ2R4; }
            set
            {
                _isCheckedQ2R4 = value;
                OnPropertyChanged(nameof(IsCheckedQ2R4));
            }
        }
        private bool _isCheckedQ2R5;
        public bool IsCheckedQ2R5
        {
            get { return _isCheckedQ2R5; }
            set
            {
                _isCheckedQ2R5 = value;
                OnPropertyChanged(nameof(IsCheckedQ2R5));
            }
        }
        private bool _isCheckedQ3R1;
        public bool IsCheckedQ3R1
        {
            get { return _isCheckedQ3R1; }
            set
            {
                _isCheckedQ3R1 = value;
                OnPropertyChanged(nameof(IsCheckedQ3R1));
            }
        }
        private bool _isCheckedQ3R2;
        public bool IsCheckedQ3R2
        {
            get { return _isCheckedQ3R2; }
            set
            {
                _isCheckedQ3R2 = value;
                OnPropertyChanged(nameof(IsCheckedQ3R2));
            }
        }
        private bool _isCheckedQ3R3;
        public bool IsCheckedQ3R3
        {
            get { return _isCheckedQ3R3; }
            set
            {
                _isCheckedQ3R3 = value;
                OnPropertyChanged(nameof(IsCheckedQ3R3));
            }
        }
        private bool _isCheckedQ3R4;
        public bool IsCheckedQ3R4
        {
            get { return _isCheckedQ3R4; }
            set
            {
                _isCheckedQ3R4 = value;
                OnPropertyChanged(nameof(IsCheckedQ3R4));
            }
        }
        private bool _isCheckedQ3R5;
        public bool IsCheckedQ3R5
        {
            get { return _isCheckedQ3R5; }
            set
            {
                _isCheckedQ3R5 = value;
                OnPropertyChanged(nameof(IsCheckedQ3R5));
            }
        }
        private bool _isCheckedQ4R1;
        public bool IsCheckedQ4R1
        {
            get { return _isCheckedQ4R1; }
            set
            {
                _isCheckedQ4R1 = value;
                OnPropertyChanged(nameof(IsCheckedQ4R1));
            }
        }
        private bool _isCheckedQ4R2;
        public bool IsCheckedQ4R2
        {
            get { return _isCheckedQ4R2; }
            set
            {
                _isCheckedQ4R2 = value;
                OnPropertyChanged(nameof(IsCheckedQ4R2));
            }
        }
        private bool _isCheckedQ4R3;
        public bool IsCheckedQ4R3
        {
            get { return _isCheckedQ4R3; }
            set
            {
                _isCheckedQ4R3 = value;
                OnPropertyChanged(nameof(IsCheckedQ4R3));
            }
        }
        private bool _isCheckedQ4R4;
        public bool IsCheckedQ4R4
        {
            get { return _isCheckedQ4R4; }
            set
            {
                _isCheckedQ4R4 = value;
                OnPropertyChanged(nameof(IsCheckedQ4R4));
            }
        }
        private bool _isCheckedQ4R5;
        public bool IsCheckedQ4R5
        {
            get { return _isCheckedQ4R5; }
            set
            {
                _isCheckedQ4R5 = value;
                OnPropertyChanged(nameof(IsCheckedQ4R5));
            }
        }
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        public SaveHospitalSurveyCommand SaveHospitalSurveyCommand { get; }

        public HospitalSurveyViewModel(Patient patient)
        {
            Patient = patient;
            Question1 = "Hospital service quality: ";
            Question2 = "Cleanliness in the hospital: ";
            Question3 = "How do you like our services? ";
            Question4 = "How likely are you to recommend our services to a friend?";
            Comment = "";
            SaveHospitalSurveyCommand = new SaveHospitalSurveyCommand(this);
        }

        public void InitializeEmptyHospitalSurvey()
        {
            SurveyService _service = new SurveyService();
            Comment = "";
            _service.ClearQuestionRatings(IsCheckedQ1R1, IsCheckedQ1R2, IsCheckedQ1R3, IsCheckedQ1R4, IsCheckedQ1R5);
            _service.ClearQuestionRatings(IsCheckedQ2R1, IsCheckedQ2R2, IsCheckedQ2R3, IsCheckedQ2R4, IsCheckedQ2R5);
            _service.ClearQuestionRatings(IsCheckedQ3R1, IsCheckedQ3R2, IsCheckedQ3R3, IsCheckedQ3R4, IsCheckedQ3R5);
            _service.ClearQuestionRatings(IsCheckedQ4R1, IsCheckedQ4R2, IsCheckedQ4R3, IsCheckedQ4R4, IsCheckedQ4R5);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
