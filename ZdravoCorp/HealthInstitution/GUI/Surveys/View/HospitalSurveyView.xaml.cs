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
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.Surveys.ViewModel;

namespace ZdravoCorp.HealthInstitution.GUI.Surveys.View
{
    /// <summary>
    /// Interaction logic for HospitalSurveyView.xaml
    /// </summary>
    public partial class HospitalSurveyView : Window
    {
        public HospitalSurveyView(Patient patient)
        {
            InitializeComponent();
            DataContext = new HospitalSurveyViewModel(patient);
        }
    }
}
