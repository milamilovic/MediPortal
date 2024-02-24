using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Model
{

    public class PatientCareRoomItem
    {
        public int roomId;
        public int numberOfPatients;
        public int MaxPatients;

        public PatientCareRoomItem(int roomId, int numberOfPatients, int maxPatients)
        {
            this.roomId = roomId;
            this.numberOfPatients = numberOfPatients;
            MaxPatients = maxPatients;
        }
    }
}
