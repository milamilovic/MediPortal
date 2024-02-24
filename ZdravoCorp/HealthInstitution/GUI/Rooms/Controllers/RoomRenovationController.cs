using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using ZdravoCorp.HealthInstitution.GUI;
using ZdravoCorp.HealthInstitution.GUI.CRUD;
using ZdravoCorp.HealthInstitution.GUI.Equipment.Controllers;
using ZdravoCorp.HealthInstitution.Core.Rooms.Services;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;

namespace ZdravoCorp.HealthInstitution.GUI.Rooms.Controllers
{
    public class RoomRenovationController
    {
        public static TableRow MakeRowForEquipmentDisplay(Room room)
        {
            TableRow row = new TableRow();

            row.FontSize = 15;

            TableCell cell = new TableCell();
            cell.Blocks.Add(new Paragraph(new Run(room.Id.ToString())));
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Blocks.Add(new Paragraph(new Run(RoomSevice.Format(room.RoomType))));
            row.Cells.Add(cell);

            return row;
        }

        public static void Initialize(Table renovationTable, ComboBox roomsCombo)
        {
            InitializeTable(renovationTable);
            InitializeCombo(roomsCombo);
        }

        public static void Initialize(Table renovationTable, ComboBox firstRoomCombo, ComboBox secondRoomCombo, ComboBox roomSplitCombo, ComboBox roomTypesCombo)
        {
            RoomUpdatingService.Update(false);
            InitializeTable(renovationTable);
            InitializeCombo(firstRoomCombo);
            InitializeCombo(secondRoomCombo);
            InitializeCombo(roomSplitCombo);
            InitializeRoomTypeCombo(roomTypesCombo);
        }

        public static void InitializeCombo(ComboBox roomsCombo)
        {
            List<int> roomIds = RoomSevice.GetAllRoomIds(false);
            foreach (int roomId in roomIds)
            {
                roomsCombo.Items.Add(roomId);
            }
        }

        public static void InitializeRoomTypeCombo(ComboBox roomsCombo)
        {
            List<Room.Type> roomTypes = RoomSevice.GetRoomTypesWithoutWarehouse();
            foreach (Room.Type type in roomTypes)
            {
                roomsCombo.Items.Add(RoomSevice.Format(type));
            }
        }

        public static void InitializeTable(Table renovationTable)
        {
            List<Room> rooms = RoomSevice.GetAllRooms(false);
            foreach (Room room in rooms)
            {
                TableRowGroup rowGroup = EquipmentGuiUtils.GetRowGroup(renovationTable);
                if (rowGroup == null) return;
                TableRow row = MakeRowForEquipmentDisplay(room);
                rowGroup.Rows.Add(row);
            }
        }
    }
}
