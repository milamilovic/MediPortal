using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using ZdravoCorp.HealthInstitution.Core.Rooms.Services;

namespace ZdravoCorpCLI
{
    public class RoomRenovationCLI
    {
        public static void RoomRenovation()
        {
            RoomUpdatingService.Update(true);
            Console.WriteLine("Type '1' for room splitting, '2' for room combining or 'x' to return: ");
            string choice;
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RoomSplitting();
                    break;
                case "2":
                    RoomCombining();
                    break;
                case "x":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Input not recognized, please try again");
                    break;
            }

        }

        public static void RoomSplitting()
        {
            Console.WriteLine("Please note that the room type of new room is inherited.");
            int roomId = InputtingRoom("Please input id of room you want to split: ");
            if (roomId == 0)
            {
                Console.WriteLine("Invalid room id!");
                return;
            }
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            InputtingDates(ref startDate, ref endDate);
            bool keepEquipment = InputtingChoice("Do you want to keep equipment in the first room?");
            RoomSplittingService service = new RoomSplittingService();
            service.SplitRoom(roomId, startDate, endDate, keepEquipment, true);
            Console.WriteLine("Room splitting is scheduled!");
        }

        public static void RoomCombining()
        {
            int firstRoomId = InputtingRoom("Please input id of the first room you want to cobine: ");
            if (firstRoomId == 0)
            {
                Console.WriteLine("Invalid room id!");
                return;
            }
            int secondRoomId = InputtingRoom("Please input id of the second room you want to cobine: ");
            if (secondRoomId == 0)
            {
                Console.WriteLine("Invalid room id!");
                return;
            }
            if (firstRoomId == secondRoomId)
            {
                Console.WriteLine("You can not combine the same room!");
                return;
            }
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            InputtingDates(ref startDate, ref endDate);
            bool keepEquipment = InputtingChoice("Do you want to keep equipment in the first room?");
            Room.Type roomType = InputingRoomType();
            if (roomType == Room.Type.Warehouse) return;
            RoomCombiningService service  = new RoomCombiningService();
            service.RoomCombining(firstRoomId, secondRoomId, startDate, endDate, roomType, keepEquipment, true);
            Console.WriteLine("Room combining is scheduled!");
        }

        public static void InputtingDates(ref DateTime startDate, ref DateTime endDate)
        {
            while (true)
            {
                Console.WriteLine("Enter the start date (dd.MM.yyyy.):");
                if (DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    if (startDate > DateTime.Today) break;
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid start date. Please try again.");
                }
            }

            while (true)
            {
                Console.WriteLine("Enter the end date (dd.MM.yyyy.):");
                if (DateTime.TryParse(Console.ReadLine(), out endDate))
                {
                    if (endDate > startDate)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("End date must be greater thanthe start date. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid end date. Please try again.");
                }
            }

        }

        public static int InputtingRoom(string question)
        {
            List<int> roomIds = GetRoomIds();
            DisplayRoomIds(roomIds);

            Console.WriteLine(question);

            int roomId = GetValidRoomId(roomIds);

            if (IsWarehouse(roomId))
            {
                Console.WriteLine("You cannot renovate the warehouse!");
                roomId = 0;
            }

            return roomId;
        }

        public static List<int> GetRoomIds()
        {
            return RoomSevice.GetAllRoomIds(true);
        }

        public static void DisplayRoomIds(List<int> roomIds)
        {
            Console.WriteLine("Room ids: ");
            foreach (int roomId in roomIds)
            {
                Console.WriteLine(roomId);
            }
        }

        public static int GetValidRoomId(List<int> roomIds)
        {
            int roomId = 0;
            bool validValue = false;
            while (!validValue)
            {
                string input = Console.ReadLine();
                validValue = int.TryParse(input, out roomId);

                if (!validValue)
                {
                    Console.WriteLine("Invalid input! Please enter a valid room ID.");
                }
                else if (!roomIds.Contains(roomId))
                {
                    Console.WriteLine("Invalid room ID! Please enter a valid room ID.");
                    validValue = false;
                }
            }

            return roomId;
        }

        public static bool IsWarehouse(int roomId)
        {
            return RoomSevice.GetRoomTypeById(roomId, true) == Room.Type.Warehouse;
        }

        public static Room.Type InputingRoomType()
        {
            List<Room.Type> roomTypes = RoomSevice.GetRoomTypesWithoutWarehouse();

            Console.WriteLine("Available room types: ");
            for (int index = 0; index < roomTypes.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {roomTypes[index]}");
            }

            string choice = Console.ReadLine();
            if (int.TryParse(choice, out int choiceNum) && choiceNum >= 1 && choiceNum <= roomTypes.Count)
            {
                return roomTypes[choiceNum - 1];
            }

            Console.WriteLine("Invalid input!");
            return Room.Type.Warehouse;
        }

        public static bool InputtingChoice(string question)
        {
            Console.WriteLine(question);
            Console.WriteLine("Input 'y' or 'yes' for yes, anything else for no: ");
            string choice = Console.ReadLine()?.Trim().ToUpper();

            return choice == "Y" || choice == "YES";
        }

    }
}
