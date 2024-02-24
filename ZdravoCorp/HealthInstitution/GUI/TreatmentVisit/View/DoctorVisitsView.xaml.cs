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
using ZdravoCorp.HealthInstitution.GUI.TreatmentVisit.ViewModel;

namespace ZdravoCorp.HealthInstitution.GUI.DoctorWindows.mvvm
{
    /// <summary>
    /// Interaction logic for DoctorVisitsView.xaml
    /// </summary>
    public partial class DoctorVisitsView : Window
    {
        public DoctorVisitsView(int docId)
        {
            InitializeComponent();
            DoctorVisitsViewModel viewModel = new DoctorVisitsViewModel(docId);
            DataContext = viewModel;
        }
    }
}
