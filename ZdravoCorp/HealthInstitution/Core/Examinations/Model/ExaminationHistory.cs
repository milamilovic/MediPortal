using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Examinations.Model
{
    internal class ExaminationHistory
    {
        public string ModificationDate { get; set; }
        public Examination Examination { get; set; }

        public ExaminationHistory(string modificationDate, Examination examination)
        {
            ModificationDate = modificationDate;
            Examination = examination;
        }

        public static ExaminationHistory[] LoadExaminationHistory(string filename)
        {
            var jsontext = File.ReadAllText(filename);
            ExaminationHistory[] allExaminations = JsonConvert.DeserializeObject<ExaminationHistory[]>(jsontext)!;
            return allExaminations;
        }

        public static void SaveExaminationHistory(ExaminationHistory[] allExaminations)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string examsJson = System.Text.Json.JsonSerializer.Serialize(allExaminations, options);
            File.WriteAllText("../../../Data/Examinations/ExaminationsHistoryData.json", examsJson);
            //File.WriteAllText(@"../../../Data/ExaminationsHistoryData.json", JsonConvert.SerializeObject(allExaminations, Formatting.Indented));
        }
    }
}
