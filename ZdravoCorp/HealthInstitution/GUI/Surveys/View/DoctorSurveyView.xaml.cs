using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZdravoCorp.HealthInstitution.Core.Surveys.Commands;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel;

namespace ZdravoCorp.HealthInstitution.GUI.PatientWindows
{
    public partial class DoctorSurveyView : Window
    {
        public DoctorSurveyView(Doctor doctor, Patient patient)
        {
            InitializeComponent();
            DataContext = new DoctorSurveyViewModel(doctor, patient);
        }
    }
}
