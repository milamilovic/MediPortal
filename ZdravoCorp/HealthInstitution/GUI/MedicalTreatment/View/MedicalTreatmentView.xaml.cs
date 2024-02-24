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
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.CRUD;
using ZdravoCorp.HealthInstitution.GUI.MedicalTreatment.ViewModel;

namespace ZdravoCorp.HealthInstitution.MedicalTreatment
{
    /// <summary>
    /// Interaction logic for MedicalTreatmentView.xaml
    /// </summary>
    public partial class MedicalTreatmentView : Window
    {
        public MedicalTreatmentView(Patient patient, MedicalTreatmentReferral referral)
        {
            InitializeComponent();
            MedicalTreatmentViewModel viewModel = new MedicalTreatmentViewModel(patient, referral);
            DataContext = viewModel;
        }
    }
}
