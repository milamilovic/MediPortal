using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Medicines.Model
{
    public class Medicine : INotifyPropertyChanged
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public List<string> Ingredients { get; set; }
        [JsonProperty]
        public int Price { get; set; }
        [JsonProperty]
        public int Quantity { get; set; }
        [JsonProperty]
        public string LastRestocked { get; set; }
        [JsonProperty]
        public int RestockingQuantity { get; set; }



        public static Medicine[] LoadFile()
        {
            var jsontext = File.ReadAllText("../../../Data/Medicines/Medicine.json");
            Medicine[] medicine = JsonConvert.DeserializeObject<Medicine[]>(jsontext)!;
            return medicine;
        }
        public static void WriteFile(Medicine[] medicines)
        {
            File.WriteAllText(@"../../../Data/Medicines/Medicine.json", JsonConvert.SerializeObject(medicines, Formatting.Indented));

        }
        public static Medicine FindMedicine(string medicineName)
        {
            Medicine[] medicines = LoadFile();
            Medicine medicine = null;
            foreach (Medicine med in medicines)
            {
                if (med.Name == medicineName)
                {
                    medicine = med;
                    return medicine;
                }
            }
            return medicine;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
