using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Surveys.Model;

namespace ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel
{
    public class HospitalAnalysisListViewModel : INotifyPropertyChanged
    {
        internal HospitalAnalysisListViewModel(HospitalSurveyAnalysisItem item)
        {
            Average = item.average;
            Ones = item.ones;
            Twos = item.twos;
            Threes = item.threes;
            Fives = item.fives;
            Fours = item.fours;
            Question = item.question;
        }

        public double Average { get; set; }
        public int Fives { get; set; }
        public int Fours { get; set; }
        public int Threes { get; set; }
        public int Twos { get; set; }
        public int Ones { get; set; }
        public string Question { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
