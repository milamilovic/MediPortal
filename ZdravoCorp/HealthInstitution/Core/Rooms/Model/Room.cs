using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Model
{
    public class Room
    {
        public int Id;
        public enum Type { OperatingRoom, PatientCareRoom, ConsultingRoom, WaitingRoom, Warehouse }

        public Type RoomType { get; set; }

        public int MaxPatientNumber { get; set; }

        public int CurrentPatientNumber { get; set; }
        public Room(Type type, int id)
        {
            Id = id;
            RoomType = type;
            InitializeNumOfPatients(type);
        }

        public void InitializeNumOfPatients(Type type)
        {
            switch (type)
            {
                case Type.OperatingRoom:
                    MaxPatientNumber = 1;
                    break;
                case Type.PatientCareRoom:
                    MaxPatientNumber = 3;
                    break;
                case Type.ConsultingRoom:
                    MaxPatientNumber = 1;
                    break;
                case Type.WaitingRoom:
                    MaxPatientNumber = 100;
                    break;
                case Type.Warehouse:
                    MaxPatientNumber = 0;
                    break;
            }
            CurrentPatientNumber = 0;
        }
    }
}
