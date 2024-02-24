using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Surveys.Model
{
    public class Comment : INotifyPropertyChanged
    {
        public Comment(string comment) { Text = comment; }

        public string Text { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
