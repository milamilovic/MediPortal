using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Equipment.Services;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using ZdravoCorp.HealthInstitution.Core.Schedules.Model;

namespace ZdravoCorp.HealthInstitution.Core.Rooms.Services
{
    public class RoomUpdatingService
    {
        public static void Update(bool forCli)
        {
            List<Room> allRooms = RoomSevice.GetAllRooms(forCli);
            List<RoomCombiningSchedule> roomsForFutureCombining = ProcessCombiningRenovations(ref allRooms, forCli);
            List<RoomSplittingSchedule> roomsForFutureSplitting = ProcessSplittingRenovations(ref allRooms, forCli);
            List<RoomSchedule> roomsForFutureRenovating = ProcessFutureRenovations(forCli);

            SaveRenovationData(roomsForFutureCombining, roomsForFutureSplitting, roomsForFutureRenovating, forCli);
            SaveRoomsData(allRooms, forCli);
        }

        private static List<RoomCombiningSchedule> ProcessCombiningRenovations(ref List<Room> allRooms, bool forCli)
        {
            List<RoomCombiningSchedule> roomsForCombining = RoomCombiningSchedule.GetCombiningRenovations(forCli);
            List<RoomCombiningSchedule> roomsForFutureCombining = new List<RoomCombiningSchedule>();
            foreach (var combiningSchedule in roomsForCombining)
            {
                DateTime endOfRenovation = TimeSlot.GetEndTime(combiningSchedule.Time);
                if (endOfRenovation < DateTime.Now)
                {
                    RemoveRoomById(allRooms, combiningSchedule.firstRoomId);
                    RemoveRoomById(allRooms, combiningSchedule.secondRoomId);
                    EquipmentMovingService.UpdateMoveRequests(false);
                    EquipmentStorageService.RemoveRoom(combiningSchedule.secondRoomId);
                    allRooms.Add(combiningSchedule.resultingRoom);
                }
                else
                {
                    roomsForFutureCombining.Add(combiningSchedule);
                }
            }
            return roomsForFutureCombining;
        }

        private static List<RoomSplittingSchedule> ProcessSplittingRenovations(ref List<Room> allRooms, bool forCli)
        {
            List<RoomSplittingSchedule> roomsForSplitting = RoomSplittingSchedule.GetSplittingRenovations(forCli);
            List<RoomSplittingSchedule> roomsForFutureSplitting = new List<RoomSplittingSchedule>();
            foreach (var splittingSchedule in roomsForSplitting)
            {
                DateTime endOfRenovation = TimeSlot.GetEndTime(splittingSchedule.Time);
                if (endOfRenovation > DateTime.Now)
                {
                    roomsForFutureSplitting.Add(splittingSchedule);
                }
                else
                {
                    EquipmentMovingService.UpdateMoveRequests(forCli);
                    EquipmentStorageService.AddRoom(splittingSchedule.resultingRoom.Id, splittingSchedule.resultingRoom.RoomType, forCli);
                    allRooms.Add(splittingSchedule.resultingRoom);
                }
            }
            return roomsForFutureSplitting;
        }

        private static List<RoomSchedule> ProcessFutureRenovations(bool forCli)
        {
            List<RoomSchedule> roomsForRenovating = RoomSchedule.GetAppointments(forCli);
            List<RoomSchedule> roomsForFutureRenovating = new List<RoomSchedule>();
            foreach (var renovatingSchedule in roomsForRenovating)
            {
                DateTime endOfRenovation = TimeSlot.GetEndTime(renovatingSchedule.Time);
                if (endOfRenovation > DateTime.Now)
                {
                    roomsForFutureRenovating.Add(renovatingSchedule);
                }
            }
            return roomsForFutureRenovating;
        }

        private static void RemoveRoomById(List<Room> rooms, int roomId)
        {
            Room roomForRemoval = rooms.Find(r => r.Id == roomId);
            if (roomForRemoval != null)
            {
                rooms.Remove(roomForRemoval);
            }
        }

        private static void SaveRenovationData(List<RoomCombiningSchedule> roomsForFutureCombining, List<RoomSplittingSchedule> roomsForFutureSplitting, List<RoomSchedule> roomsForFutureRenovating, bool forCli)
        {
            RoomCombiningSchedule.Save(roomsForFutureCombining, forCli);
            RoomSplittingSchedule.Save(roomsForFutureSplitting, forCli);
            RoomSchedule.SaveSimpleAppointments(roomsForFutureRenovating, forCli);
        }

        public static void SaveRoomsData(List<Room> allRooms, bool forCli)
        {
            if (!forCli)
            {
                RoomSevice.Serialize(allRooms, "../../../Data/Rooms/Rooms.json");
            } else
            {
                RoomSevice.Serialize(allRooms, "../../../../ZdravoCorp/Data/Rooms/Rooms.json");
            }
        }

    }
}
