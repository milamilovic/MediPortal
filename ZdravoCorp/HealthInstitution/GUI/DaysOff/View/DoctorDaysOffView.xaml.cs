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
using ZdravoCorp.HealthInstitution.GUI.DaysOff.ViewModel;

namespace ZdravoCorp.HealthInstitution.GUI.DoctorWindows.DoctorDaysOff
{
    /// <summary>
    /// Interaction logic for DoctorDaysOffView.xaml
    /// </summary>
    public partial class DoctorDaysOffView : Window
    {
        public DoctorDaysOffView(int docId)
        {
            InitializeComponent();
            DoctorDaysOffViewModel viewModel = new DoctorDaysOffViewModel(docId);
            DataContext = viewModel;
        }
    }
}
