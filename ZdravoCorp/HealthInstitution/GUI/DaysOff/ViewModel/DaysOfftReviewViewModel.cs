using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Commands;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Model;
using ZdravoCorp.HealthInstitution.DaysOff.Repository;

namespace ZdravoCorp.HealthInstitution.GUI.DaysOff.ViewModel
{
    public class DaysOfftReviewViewModel : INotifyPropertyChanged
    {

        private readonly ObservableCollection<DaysOffRequestListItemViewModel> _requests;
        public IEnumerable<DaysOffRequestListItemViewModel> Requests => _requests;
        private DaysOffRequestListItemViewModel _selectedRequest { get; set; }
        public DaysOffRequestListItemViewModel SelectedRequest { get => _selectedRequest; set => _selectedRequest = value; }



        public ApproveDaysOffCommand ApproveCommand { get; }
        public RejectDaysOffCommand RejectCommand { get; }


        private int _selected;
        public int Selected
        {
            get => _selected;
            set
            {
                if (value < 0) { return; }
                _selected = value;
                OnPropertyChanged(nameof(Selected));
                _selectedRequest = _requests.ElementAt(_selected);
            }
        }

        public DaysOfftReviewViewModel()
        {
            _requests = new ObservableCollection<DaysOffRequestListItemViewModel>();
            InitializeRequests();
            ApproveCommand = new ApproveDaysOffCommand(this);
            RejectCommand = new RejectDaysOffCommand(this);
        }

        public void InitializeRequests()
        {
            _requests.Clear();
            List<DaysOffRequest> daysOff = DaysOffRepository.Deserialize();
            foreach (DaysOffRequest dayOff in daysOff)
            {
                if (!dayOff.Approved)
                {
                    _requests.Add(new DaysOffRequestListItemViewModel(dayOff));
                }
            }
            if (_requests.Count != 0)
            {
                Selected = 0;
                OnPropertyChanged(nameof(Selected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
