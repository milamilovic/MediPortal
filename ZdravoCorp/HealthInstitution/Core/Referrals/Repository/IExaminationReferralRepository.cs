using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;

namespace ZdravoCorp.HealthInstitution.Core.Referrals.Repository
{
    public interface IExaminationReferralRepository
    {
        ExaminationReferral[] LoadFile();
        void WriteFile(ExaminationReferral[] referrals);
        void AddExaminationReferral(ExaminationReferral referral);

  
    }
}
