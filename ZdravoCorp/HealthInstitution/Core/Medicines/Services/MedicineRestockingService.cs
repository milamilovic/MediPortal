using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Medicines.Model;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;

namespace ZdravoCorp.HealthInstitution.Core.Medicines.Services
{
    internal class MedicineRestockingService
    {
        public static void RestockMedicine()
        {
            DateTime now = DateTime.Now;
            Medicine[] medicines = Medicine.LoadFile();
            foreach (Medicine medicine in medicines)
            {
                DateTime lastRestocked = DateTime.ParseExact(medicine.LastRestocked, "dd.MM.yyyy.", null);
                TimeSpan difference = now.Subtract(lastRestocked);
                int daysDifference = (int)difference.TotalDays;
                medicine.Quantity += daysDifference * medicine.RestockingQuantity;
                medicine.LastRestocked = now.ToString("dd.MM.yyyy.");
            }
            Medicine.WriteFile(medicines);


            Prescription[] prescriptions = Prescription.LoadFile();
            foreach (Prescription pe in prescriptions)
            {
                foreach (Medicine m in medicines)
                {
                    if (m.Name == pe.Medicine.Name)
                        pe.Medicine = m;
                }
            }
            Prescription.WriteFile(prescriptions);
        }
    }
}
