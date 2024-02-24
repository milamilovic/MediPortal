using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;

namespace ZdravoCorp.HealthInstitution.Core.Referrals.Repository
{
    public class ExaminationReferralRepository : IExaminationReferralRepository
    {
        public ExaminationReferral[] LoadFile()
        {
            var jsontext = File.ReadAllText("../../../Data/Referrals/ExaminationReferral.json");
            ExaminationReferral[] referrals = JsonConvert.DeserializeObject<ExaminationReferral[]>(jsontext)!;
            return referrals;
        }
        public void WriteFile(ExaminationReferral[] referrals)
        {
            string fileName = "../../../Data/Referrals/ExaminationReferral.json";
            dynamic text = JsonConvert.SerializeObject(referrals, Formatting.Indented);
            File.WriteAllText(fileName, text);

        }
        public void AddExaminationReferral(ExaminationReferral examinationReferral)
        {
            ExaminationReferral[] referrals = LoadFile();
            referrals = referrals.Concat(new ExaminationReferral[] { examinationReferral }).ToArray();
            WriteFile(referrals);
        }


    }
}
