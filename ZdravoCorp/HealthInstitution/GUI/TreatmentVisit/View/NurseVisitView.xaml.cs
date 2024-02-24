using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using ZdravoCorp.HealthInstitution.Core.TreatmentVisit.Model;
using ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel;

namespace ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.View
{
    /// <summary>
    /// Interaction logic for NurseVisitView.xaml
    /// </summary>
    public partial class NurseVisitView : Window
    {
        public NurseVisitView(PatientOnTreatmentItem patient)
        {
            InitializeComponent();
            NurseVisitViewModel viewModel = new NurseVisitViewModel(patient);
            DataContext = viewModel;
        }
    }
}
