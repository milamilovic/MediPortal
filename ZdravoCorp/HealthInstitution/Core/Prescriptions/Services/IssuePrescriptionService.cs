using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Medicines.Model;
using ZdravoCorp.HealthInstitution.Core.Prescriptions.Model;

namespace ZdravoCorp.HealthInstitution.Core.Prescriptions.Services
{
    internal class IssuePrescriptionService
    {
        public static bool CheckForAllergies(Medicine medicine, string[] allergies)
        {
            foreach (string allergy in allergies)
            {
                if (medicine.Ingredients.Contains(allergy, StringComparer.OrdinalIgnoreCase) ||
                    medicine.Name.ToLower() == allergy.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        public static void IssuePrescription(Prescription prescription)
        {
            Prescription[] prescriptions = Prescription.LoadFile();
            prescriptions = prescriptions.Concat(new Prescription[] { prescription }).ToArray();
            Prescription.WriteFile(prescriptions);
        }
    }
}
