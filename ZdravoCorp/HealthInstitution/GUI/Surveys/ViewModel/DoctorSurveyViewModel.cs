using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Surveys.Commands;
using ZdravoCorp.HealthInstitution.Core.Surveys.Services;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel
{
    public class DoctorSurveyViewModel : INotifyPropertyChanged
    {
        public Doctor Doctor;
        public Patient Patient;
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Doctor.DoctorsSpeciality Speciality { get; set; }
        private string _titleLabel;
        public string TitleLabel
        {
            get { return _titleLabel; }
            set
            {
                _titleLabel = value;
                OnPropertyChanged(nameof(TitleLabel));
            }
        }
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
        public SaveDoctorSurveyCommand SaveDoctorSurveyCommand { get; }
        public DoctorSurveyViewModel(Doctor doctor, Patient patient)
        {
            Doctor = doctor;
            Patient = patient;
            Question1 = "Doctor service quality: ";
            Question2 = "How likely are you to recommend the doctors services to a friend?";
            Comment = "";
            SaveDoctorSurveyCommand = new SaveDoctorSurveyCommand(this);
            InitializeData();
        }

        public void InitializeData()
        {
            TitleLabel = "Doctor id: " + Doctor.Id.ToString();
            Username = Doctor.Username;
            Name = Doctor.Name;
            Surname = Doctor.Surname;
            Speciality = Doctor.Speciality;
        }

        public void InitializeEmptyDoctorSurvey()
        {
            SurveyService _service = new SurveyService();
            Comment = "";
            _service.ClearQuestionRatings(IsCheckedQ1R1, IsCheckedQ1R2, IsCheckedQ1R3, IsCheckedQ1R4, IsCheckedQ1R5);
            _service.ClearQuestionRatings(IsCheckedQ2R1, IsCheckedQ2R2, IsCheckedQ2R3, IsCheckedQ2R4, IsCheckedQ2R5);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
