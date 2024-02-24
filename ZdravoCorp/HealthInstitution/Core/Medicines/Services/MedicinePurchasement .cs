using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Medicines.Model;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;

namespace ZdravoCorp.HealthInstitution.Core.Medicines.Services
{
    internal class MedicinePurchasement
    {
        public static void PurchaseMedicine(Patient patient, Prescription prescription)
        {
            Medicine[] medicines = Medicine.LoadFile();
            Medicine changedMedicine = medicines[0];
            foreach (Medicine me in medicines)
            {
                if (me.Name == prescription.Medicine.Name)
                {
                    me.Quantity -= 1;
                    changedMedicine = me;
                    break;
                }
            }
            Medicine.WriteFile(medicines);

            Prescription[] prescriptions = Prescription.LoadFile();
            foreach (Prescription p in prescriptions)
            {
                if (p.PatientId == prescription.PatientId && p.Date.Equals(prescription.Date) && p.Medicine.Name.Equals(prescription.Medicine.Name))
                {
                    p.WasUsed = true;
                }
                if (p.Medicine.Name.Equals(changedMedicine.Name))
                    p.Medicine = changedMedicine;
            }


            Prescription.WriteFile(prescriptions);



        }
        public static Prescription FindPrescription(Patient patient)
        {
            Prescription[] prescriptions = Prescription.LoadFile();
            foreach (Prescription prescription in prescriptions)
            {
                if (prescription.PatientId == patient.Id && !prescription.WasUsed)
                {
                    return prescription;
                }
            }

            return null;
        }
        public static bool IsLastDosageTaken(Prescription prescription)
        {
            List<Prescription> patientsPrescriptions = new List<Prescription>();

            Prescription[] prescriptions = Prescription.LoadFile();
            foreach (Prescription p in prescriptions)
            {
                if (p.PatientId == prescription.PatientId && p.Medicine.Name.Equals(prescription.Medicine.Name) && p.WasUsed)
                {
                    patientsPrescriptions.Add(p);
                }
            }
            if (patientsPrescriptions.Count == 0) return true;

            Prescription mostRecentPrescription = patientsPrescriptions[0];
            foreach (Prescription p in patientsPrescriptions)
            {
                DateTime mostRecentDate = DateTime.ParseExact(mostRecentPrescription.Date, "dd.MM.yyyy.", null);
                DateTime currentDate = DateTime.ParseExact(p.Date, "dd.MM.yyyy.", null);
                if (mostRecentDate.CompareTo(currentDate) <= 0)
                {
                    mostRecentPrescription = p;
                }
            }

            DateTime now = DateTime.Now;
            DateTime mostRecentPrescriptionDate = DateTime.ParseExact(mostRecentPrescription.Date, "dd.MM.yyyy.", null);
            TimeSpan difference = now.Subtract(mostRecentPrescriptionDate);
            int daysDifference = (int)difference.TotalDays;

            if (mostRecentPrescription.Days - daysDifference > 1)
            {
                return false;
            }

            return true;
        }

        public static bool IsMedicineStocked(Medicine medicine)
        {
            Medicine[] medicines = Medicine.LoadFile();
            foreach (Medicine me in medicines)
            {
                if (me.Name == medicine.Name && me.Quantity > 0) return true;
            }
            return false;
        }

    }
}
