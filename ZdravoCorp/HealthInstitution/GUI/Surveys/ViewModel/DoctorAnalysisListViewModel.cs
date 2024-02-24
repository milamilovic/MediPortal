using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;
using ZdravoCorp.HealthInstitution.Surveys;

namespace ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel
{
    public class DoctorAnalysisListViewModel : INotifyPropertyChanged
    {
        internal DoctorAnalysisListViewModel(DoctorSurveyAnalysisItem item)
        {
            Average = item.average;
            Ones = item.ones;
            Twos = item.twos;
            Threes = item.threes;
            Fives = item.fives;
            Fours = item.fours;
            Question = item.question;
            DoctorId = item.doctorId;
        }

        public double Average { get; set; }
        public int Fives { get; set; }
        public int Fours { get; set; }
        public int Threes { get; set; }
        public int Twos { get; set; }
        public int Ones { get; set; }
        public string Question { get; set; }
        public int DoctorId { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
