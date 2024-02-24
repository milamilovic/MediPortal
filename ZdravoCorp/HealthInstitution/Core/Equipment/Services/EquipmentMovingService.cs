using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.HealthInstitution.Core.Equipment.Model;

namespace ZdravoCorp.HealthInstitution.Core.Equipment.Services
{
    public class EquipmentMovingService
    {
        public static bool RequestMoving(string equipmentName, int originRoomId, int destinationRoomId, int quantity, DateTime time, bool forCli)
        {
            equipmentName = EquipmentService.UndoFormat(equipmentName);
            EquipmentMovingRequest request = new EquipmentMovingRequest(equipmentName, originRoomId, destinationRoomId, quantity, time);
            if (EquipmentService.IsEquipmentDinamicByName(equipmentName))
            {
                MoveEquipment(request);
            }
            else
            {
                ScheduleRequest(request, forCli);
            }
            return true;
        }

        public static bool RequestMovingWithSchedulingDynamic(string equipmentName, int originRoomId, int destinationRoomId, int quantity, DateTime time, bool forCli)
        {
            equipmentName = EquipmentService.UndoFormat(equipmentName);
            EquipmentMovingRequest request = new EquipmentMovingRequest(equipmentName, originRoomId, destinationRoomId, quantity, time);
            ScheduleRequest(request, forCli);
            return true;
        }

        public static void MoveEquipment(EquipmentMovingRequest request)
        {
            EquipmentStorageService.ChangeItemQuantityInRoom(request.EquipmentName, request.OriginRoomId, request.Quantity * -1);
            EquipmentStorageService.ChangeItemQuantityInRoom(request.EquipmentName, request.DestinationRoomId, request.Quantity);
        }

        private static List<EquipmentMovingRequest> GetAllRequests(string fileName)
        {
            while (true)
            {
                try
                {
                    using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var jsontext = File.ReadAllText(fileName);
                        List<EquipmentMovingRequest> allRequests = JsonConvert.DeserializeObject<List<EquipmentMovingRequest>>(jsontext)!;
                        return allRequests;
                    }
                }
                catch
                {
                    continue;
                }
            }
            //var jsontext = File.ReadAllText(fileName);
            //List<EquipmentMovingRequest> allRequests = JsonConvert.DeserializeObject<List<EquipmentMovingRequest>>(jsontext)!;
            //return allRequests;
        }

        private static void SerializeRequests(ref List<EquipmentMovingRequest> requests, string fileName)
        {
            while (true)
            {
                try
                {
                    string text = JsonConvert.SerializeObject(requests, Formatting.Indented);
                    File.WriteAllText(fileName, text);
                    return;
                }
                catch
                {
                    continue;
                }
            }
        }

        private static void ScheduleRequest(EquipmentMovingRequest request, bool forCli)
        {
            string fileName = "../../../Data/EquipmentStorage/EquipmentMovingSchedule.json";
            if (forCli) { fileName = "../../../../ZdravoCorp/Data/EquipmentStorage/EquipmentMovingSchedule.json"; }
            List<EquipmentMovingRequest> allRequests = GetAllRequests(fileName);
            allRequests.Add(request);
            SerializeRequests(ref allRequests, fileName);
        }

        public static bool IsUpdateNeeded()
        {
            string fileName = "../../../Data/EquipmentStorage/EquipmentMovingSchedule.json";
            List<EquipmentMovingRequest> allRequests = GetAllRequests(fileName);
            if (allRequests.Count == 0)
            {
                return false;
            }
            foreach (EquipmentMovingRequest request in allRequests)
            {
                DateTime requestMovingMoment = DateTime.ParseExact(request.MovingDateTime, "MM.dd.yyyy HH:mm",
                                       CultureInfo.InvariantCulture); ;
                if (requestMovingMoment <= DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        public static void UpdateMoveRequests(bool forCli)
        {
            string fileName = "../../../Data/EquipmentStorage/EquipmentMovingSchedule.json";
            if(forCli) fileName = "../../../../ZdravoCorp/Data/EquipmentStorage/EquipmentMovingSchedule.json";
            List<EquipmentMovingRequest> allRequests = GetAllRequests(fileName);

            if (allRequests.Count == 0)
                return;

            List<EquipmentMovingRequest> unprocessedRequests = GetAllRequests(fileName);

            foreach (EquipmentMovingRequest request in allRequests)
            {
                DateTime requestMovingMoment = DateTime.ParseExact(request.MovingDateTime, "MM.dd.yyyy HH:mm", CultureInfo.InvariantCulture);

                if (requestMovingMoment <= DateTime.Now)
                {
                    UpdateEquipmentQuantities(request, unprocessedRequests);
                }
            }

            SerializeRequests(ref unprocessedRequests, fileName);
        }

        private static void UpdateEquipmentQuantities(EquipmentMovingRequest request, List<EquipmentMovingRequest> unprocessedRequests)
        {
            EquipmentStorageService.ChangeItemQuantityInRoom(request.EquipmentName, request.OriginRoomId, -request.Quantity);
            EquipmentStorageService.ChangeItemQuantityInRoom(request.EquipmentName, request.DestinationRoomId, request.Quantity);

            EquipmentMovingRequest requestToRemove = unprocessedRequests.Find(x =>
                x.EquipmentName == request.EquipmentName &&
                x.DestinationRoomId == request.DestinationRoomId &&
                x.OriginRoomId == request.OriginRoomId &&
                x.Quantity == request.Quantity);

            unprocessedRequests.Remove(requestToRemove);
        }
    }
}
