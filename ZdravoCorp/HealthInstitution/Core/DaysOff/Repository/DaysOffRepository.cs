using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.DaysOff.Model;

namespace ZdravoCorp.HealthInstitution.DaysOff.Repository
{
    internal class DaysOffRepository
    {
        public static void Serialize(List<DaysOffRequest> daysOff)
        {
            string fileName = "../../../Data/DaysOff/DaysOff.json";
            dynamic text = JsonConvert.SerializeObject(daysOff, Formatting.Indented);
            File.WriteAllText(fileName, text);
        }

        public static List<DaysOffRequest> Deserialize()
        {
            string fileName = "../../../Data/DaysOff/DaysOff.json";
            var jsontext = File.ReadAllText(fileName);
            List<DaysOffRequest> daysOff = JsonConvert.DeserializeObject<List<DaysOffRequest>>(jsontext)!;
            return daysOff;
        }

        public static void AddDayOffRequest(DaysOffRequest dayOff)
        {
            List<DaysOffRequest> daysOff = Deserialize();
            daysOff.Add(dayOff);
            Serialize(daysOff);
        }
    }
}
