using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Referrals.Model;
using ZdravoCorp.HealthInstitution.Core.Referrals.Repository;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Services
{
    internal class PatientCareRoomsServices
    {
        public static void UpdateRooms()
        {


            List<Room> rooms = RoomSevice.GetAllRooms(false);
            MedicalTreatmentReferralRepository repository = new MedicalTreatmentReferralRepository();
            MedicalTreatmentReferral[] referrals = repository.LoadFile();
            foreach (Room room in rooms)
            {
                int currentNumberOfPatients = 0;
                foreach (MedicalTreatmentReferral referral in referrals)
                {
                    if (referral.RoomId == room.Id && referral.TreatmentStarted && !referral.TreatmentEnded)
                    {
                        currentNumberOfPatients++;
                    }
                }
                room.CurrentPatientNumber = currentNumberOfPatients;
            }
            RoomUpdatingService.SaveRoomsData(rooms, false);
        }
    }
}
