using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Services
{
    public interface IComplexRenovationService
    {
        bool ValidateRooms(int firstRoomId, int secondRoomId, DateTime startDate, DateTime endDate, bool forCli);
        void ScheduleEquipmentMoving(int firstRoomId, int secondRoomId,
            DateTime startDate, DateTime endDate, bool returnEquipment, bool forCli);
    }
}
