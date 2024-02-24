using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoCorp;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Xml.Linq;
using Color = System.Windows.Media.Color;
using System;
using System.Threading;
using System.Configuration;
using ZdravoCorp.HealthInstitution.DaysOff;
using ZdravoCorp.HealthInstitution.Surveys;
using ZdravoCorp.HealthInstitution.DoctorSurveys;
using ZdravoCorp.HealthInstitution.GUI.Equipment.Controllers;
using ZdravoCorp.HealthInstitution.Core.Equipment.Model;
using ZdravoCorp.HealthInstitution.Core.Equipment.Services;
using ZdravoCorp.HealthInstitution.Core.Rooms.Services;
using ZdravoCorp.HealthInstitution.Core.Rooms.Model;
using ZdravoCorp.HealthInstitution.Core.Users.Model;
using ZdravoCorp.HealthInstitution.GUI.Rooms.Controllers;

namespace ZdravoCorp.HealthInstitution.GUI
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        Thread updatingThread;
        public ManagerMainWindow(string username)
        {
            EquipmentMovingService.UpdateMoveRequests(false);
            RoomUpdatingService.Update(false);
            InitializeThread();
            InitializeComponent();
            Manager loggedInManager = Manager.FindManagerByUsername(username);
            InitializeProfile(loggedInManager);
            InitializeTables();
            InitializeTabs();
        }

        public void InitializeTables()
        {
            InitializeEquipmentTable();
            InitializeOrderingTable();
        }

        public void InitializeThread()
        {
            updatingThread = new Thread(new ThreadStart(CheckForEquipmentUpdates));
            updatingThread.IsBackground = true;
            updatingThread.Start();
        }

        public void InitializeTabs()
        {
            InitializeEquipmentOrderingTab();
            InitializeEquipmentMoveTab();
            InitializeSimpleRenovationTabs();
        }

        public void InitializeSimpleRenovationTabs()
        {
            RoomRenovationController.Initialize(ComplexRenovationTable, FirstRoomRenovationCombo, SecondRoomRenovationCombo, RoomSplitCombo, ComplexRenovationCombiningRoomType);
            RoomRenovationController.Initialize(SimpleRenovationTable, RoomRenovationIdCombo);
        }

        public void CheckForEquipmentUpdates()
        {
            while (true)
            {
                bool updateNeeded = EquipmentMovingService.IsUpdateNeeded() || EquipmentStorageService.IsChanged;
                if (updateNeeded)
                {
                    EquipmentMovingService.UpdateMoveRequests(false);
                    EquipmentStorageService.IsChanged = false;
                    this.Dispatcher.Invoke(() =>
                    {
                        InitializeEquipmentTable();
                        UpdateEquipmentMoveTable();
                        InitializeOrderingTable();
                    });
                }
                Thread.Sleep(300);      //check 3 times in a second
            }
        }

        public void InitializeOrderingTable()
        {
            Dictionary<string, int> items = EquipmentFilteringService.GetDynamicEquipmentSumItems();
            foreach (var item in items)
            {
                TableRowGroup rowGroup = EquipmentGuiUtils.GetRowGroup(EquipmentOrderingTable);

                if (rowGroup == null) return;
                TableRow row = OrderEquipmentController.MakeRowForEquipmentDisplay(item);
                rowGroup.Rows.Add(row);
            }
        }

        public void InitializeEquipmentOrderingTab()
        {
            List<string> allEquipmentTypes = EquipmentService.GetAllEquipmentTypes();
            EquipmentGuiUtils.SetEquipmentComboBox(OrderEquipmentTypeComboBox, ref allEquipmentTypes, true);
        }

        public static void ClearTable(Table table)
        {
            try
            {
                table.RowGroups.RemoveAt(1);
            }
            catch { }
        }

        public void SetElementsEquipmentTable(List<EquipmentStorageItem> elements)
        {
            foreach (EquipmentStorageItem item in elements)
            {
                TableRowGroup rowGroup = EquipmentGuiUtils.GetRowGroup(EquipmentTableXaml);

                if (rowGroup == null) return;
                TableRow row = EquipmentFilterSearchController.MakeRowForEquipmentDisplay(item);
                rowGroup.Rows.Add(row);

            }
        }

        public void InitializeEquipmentTable()
        {
            var jsontext = File.ReadAllText("../../../Data/EquipmentStorage/EquipmentStorage.json");
            List<EquipmentStorageItem> items = JsonConvert.DeserializeObject<List<EquipmentStorageItem>>(jsontext)!;

            SetElementsEquipmentTable(items);
        }

        public void RefreshEquipmentTable(object sender, RoutedEventArgs e)
        {
            ClearTable(EquipmentTableXaml);
            InitializeEquipmentTable();
        }

        public void InitializeEquipmentMoveTab()
        {
            //initialization of equipment combo box
            List<string> allEquipmentNames = EquipmentService.GetAllEquipmentTypes();
            EquipmentGuiUtils.SetEquipmentComboBox(MovingSelectEquipmentCombo, ref allEquipmentNames, false);

            //initialization of room id combo box
            List<int> allRoomIds = RoomSevice.GetAllRoomIds(false);
            foreach (int roomId in allRoomIds)
            {
                FromRoomCombo.Items.Add(roomId);
                ToRoomCombo.Items.Add(roomId);
            }

            //equipment move table
            UpdateEquipmentMoveTable();
        }

        public void RefreshMoveTable(object sender, RoutedEventArgs e)
        {
            UpdateEquipmentMoveTable();
        }

        public void UpdateEquipmentMoveTable()
        {
            ClearTable(EquipmentMoveTable);
            var jsontext = File.ReadAllText("../../../Data/EquipmentStorage/EquipmentStorage.json");
            List<EquipmentStorageItem> items = JsonConvert.DeserializeObject<List<EquipmentStorageItem>>(jsontext)!; 
            EquipmentMovingService.UpdateMoveRequests(false);
            jsontext = File.ReadAllText("../../../Data/EquipmentStorage/EquipmentMovingSchedule.json");
            List<EquipmentMovingRequest> movingItems = JsonConvert.DeserializeObject<List<EquipmentMovingRequest>>(jsontext)!;
            SetElementsEquipmentMoveTable(items, movingItems);
        }

        public void SetElementsEquipmentMoveTable(List<EquipmentStorageItem> allEquipment, List<EquipmentMovingRequest> equipmentScheduledForMoving)
        {
            foreach (EquipmentStorageItem item in allEquipment)
            {
                TableRowGroup rowGroup = EquipmentGuiUtils.GetRowGroup(EquipmentMoveTable);

                if (rowGroup == null) return;
                TableRow row = MoveEquipmentController.MakeRowForEquipmentDisplay(item, ref equipmentScheduledForMoving);
                rowGroup.Rows.Add(row);

            }
        }

        public void MoveClick(object sender, RoutedEventArgs e)
        {
            string equipmentName = EquipmentService.Format(MovingSelectEquipmentCombo.Text);
            int fromRoom;
            int toRoom;

            if (!int.TryParse(FromRoomCombo.SelectedItem?.ToString(), out fromRoom) || !int.TryParse(ToRoomCombo.SelectedItem?.ToString(), out toRoom))
            {
                MessageBox.Show("Please select valid rooms!");
                return;
            }

            string quantityString = equipmentMoveQuantity.Text;
            string hoursAndMinutes = MoveTimePicker.Text.ToString();
            DateTime moveMoment = DateTime.Now;

            if (!EquipmentService.IsEquipmentDinamicByName(equipmentName))
            {
                if (!MoveDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please input valid date!");
                    return;
                }

                moveMoment = MoveDatePicker.SelectedDate.Value.Date;
            }

            MoveEquipmentController.MoveEquipment(equipmentName, fromRoom, toRoom, quantityString, hoursAndMinutes, moveMoment, equipmentMoveQuantity, FromRoomCombo, ToRoomCombo, MovingSelectEquipmentCombo);
            UpdateEquipmentMoveTable();
        }


        public void MovingEquipmentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedEquipment = (sender as ComboBox).SelectedValue as string;
            bool isDynamicEquipment = EquipmentService.IsEquipmentDinamicByName(selectedEquipment);

            MoveDatePicker.IsEnabled = !isDynamicEquipment;
            MoveDatePicker.IsHitTestVisible = !isDynamicEquipment;
            MoveTimePicker.IsEnabled = !isDynamicEquipment;
            MoveTimePicker.IsReadOnly = isDynamicEquipment;

            FromRoomCombo.Items.Clear();
            ToRoomCombo.Items.Clear();

            List<Room.Type> possibleRoomTypes = EquipmentService.GetValidRoomTypesForEquipment(selectedEquipment);

            foreach (int id in RoomSevice.GetAllRoomIds(false))
            {
                Room.Type roomType = RoomSevice.GetRoomTypeById(id, false);

                if (possibleRoomTypes.Contains(roomType))
                {
                    FromRoomCombo.Items.Add(id);
                    ToRoomCombo.Items.Add(id);
                }
            }
        }


        public void LogOutClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        public void InitializeProfile(Manager loggedInManager)
        {
            Profile.Background = new ImageBrush(new BitmapImage(new Uri("../../../" + loggedInManager.Image, UriKind.Relative)));

            UpdateLabelWithSpaces(name, loggedInManager.Name, 55);
            UpdateLabelWithSpaces(surname, loggedInManager.Surname, 52);
            UpdateLabelWithSpaces(username, loggedInManager.Username, 48);
            UpdateLabelWithSpaces(password, loggedInManager.Password, 49);
            UpdateLabelWithSpaces(birthday, loggedInManager.Birthday, 50);
            UpdateLabelWithSpaces(email, loggedInManager.Email, 45);
            UpdateLabelWithSpaces(role, "Manager", 55);
        }

        private void UpdateLabelWithSpaces(Label label, string value, int totalSpaces)
        {
            int spacesNeeded = totalSpaces - value.Length - label.Content.ToString().Length;
            for (int i = 0; i < spacesNeeded; i++)
            {
                label.Content += " ";
            }
            label.Content += value;
        }


        public void ApplyFilters(object sender, RoutedEventArgs e)
        {
            List<EquipmentStorageItem> items = GetEquipmentFiltered();
            ClearTable(EquipmentTableXaml);
            SetElementsEquipmentTable(items);
        }

        public List<EquipmentStorageItem> GetEquipmentFiltered()
        {
            bool warehouseChecked = CheckState(FilterRoomWarehouse);
            bool waitingChecked = CheckState(FilterRoomWaiting);
            bool consultingChecked = CheckState(FilterRoomConsulting);
            bool patientCareChecked = CheckState(FilterRoomPatientCare);
            bool operatingChecked = CheckState(FilterRoomOperating);

            bool staticChecked = CheckState(FilterEquipmentTypeStatic);
            bool dynamicChecked = CheckState(FilterEquipmentTypeDynamic);

            bool moreThanTenChecked = CheckState(FilterByQuantityMoreThanTen);
            bool upToTenChecked = CheckState(FilterByQuantityUpToTen);
            bool zeroChecked = CheckState(FilterQuantityZero);

            return EquipmentFilterSearchController.ApplyFilters(warehouseChecked, waitingChecked, consultingChecked,
                patientCareChecked, operatingChecked, staticChecked, dynamicChecked, moreThanTenChecked, upToTenChecked, zeroChecked);
        }

        private bool CheckState(CheckBox checkBox)
        {
            return checkBox.IsChecked ?? false;
        }

        public void ApplySearch(object sender, RoutedEventArgs e)
        {
            //take all items that pass the filters and search them
            //this allows for combined filtering and search
            List<EquipmentStorageItem> adequateItems = GetEquipmentFiltered();

            adequateItems = EquipmentSearchService.SearchEquipment(SearchTextBox.Text, ref adequateItems);
            SearchTextBox.Text = "";
            ClearTable(EquipmentTableXaml);
            SetElementsEquipmentTable(adequateItems);
        }

        public void OrderClick(object sender, RoutedEventArgs e)
        {
            string equipmentName = EquipmentService.Format(OrderEquipmentTypeComboBox.Text);
            string quantityBoxText = equipmentOrderQuantity.Text;
            OrderEquipmentController.PlaceOrder(equipmentName, quantityBoxText, equipmentOrderQuantity, OrderEquipmentTypeComboBox);
        }

        public void RenovationClick(object sender, RoutedEventArgs e)
        {
            RenovationUtils.SimpleRenovation(RoomRenovationIdCombo, SimpleRenovationFromDate, SimpleRenovationToDate);
        }

        public void RenovationCombineClick(object sender, RoutedEventArgs e)
        {
            RenovationUtils.CombiningRooms(FirstRoomRenovationCombo, SecondRoomRenovationCombo, ComplexRenovationFromDate,
                ComplexRenovationToDate, CombiningRoomsCheckBox, ComplexRenovationCombiningRoomType);
        }

        public void RenovationSplitClick(object sender, RoutedEventArgs e)
        {
            RenovationUtils.SplittingRoom(RoomSplitCombo, ComplexRenovationSplitFromDate, ComplexRenovationSplitToDate, CombiningRoomsCheckBox, false);
        }

        private void DaysOffButton(object sender, RoutedEventArgs e)
        {
            DaysOffRequestView daysOffRequestView = new DaysOffRequestView();
            daysOffRequestView.Show();
        }

        private void HospitalButtonClick(object sender, RoutedEventArgs e)
        {
            HospitalSurveyAnalysisView hospitalAnalysesView = new HospitalSurveyAnalysisView();
            hospitalAnalysesView.Show();
        }

        private void DoctorButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorSurveyAnalysisView doctorAnalysesView = new DoctorSurveyAnalysisView();
            doctorAnalysesView.Show();
        }
    }
}