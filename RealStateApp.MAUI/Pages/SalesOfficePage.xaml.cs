using RealStateApp.MAUI.Models;
using RealStateApp.MAUI.Services;
using System.Collections.ObjectModel;

namespace RealStateApp.MAUI.Pages
{
    public partial class SalesOfficePage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<SalesOffice> SalesOffices { get; set; }
        public ObservableCollection<SalesOfficeProperties> GroupedProperties { get; set; }
        public ObservableCollection<SalesOfficeEmployees> GroupedEmployees { get; set; }

        public SalesOfficePage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            SalesOffices = new ObservableCollection<SalesOffice>();
            GroupedProperties = new ObservableCollection<SalesOfficeProperties>();
            GroupedEmployees = new ObservableCollection<SalesOfficeEmployees>();
            BindingContext = this;
        }

        private async void OnLoadSalesOfficesClicked(object sender, EventArgs e)
        {
            GroupedEmployees.Clear();
            GroupedProperties.Clear();
            SalesOffices.Clear();
            var salesOffices = await _apiService.GetAllSalesOffices();

            foreach (var office in salesOffices)
            {
                var properties = await _apiService.GetPropertiesByOfficeID(office.OfficeID);
                office.NumberOfProperties = properties.Count();

                if (office.AddressID.HasValue)
                {
                    var address = await _apiService.GetAddress(office.AddressID.Value);
                    office.AddressLine = address.AddressLine;
                }

                if (office.ManagedByEmployeeID.HasValue)
                {
                    var employee = await _apiService.GetEmployee(office.ManagedByEmployeeID.Value);
                    office.ManagerName = $"{employee.EmpFirstName} {employee.EmpLastName}";
                }

                SalesOffices.Add(office);
            }
        }

        private async void OnAddSalesOfficeClicked(object sender, EventArgs e)
        {
            var newSalesOffice = new SalesOffice
            {
                OfficeID = int.TryParse(OfficeIdEntry.Text, out var officeId) ? officeId : 0,
                OfficeName = OfficeNameEntry.Text,
                AddressID = int.TryParse(AddressIdEntry.Text, out var addressId) ? addressId : null,
                ManagedByEmployeeID = int.TryParse(ManagedByEmployeeIdEntry.Text, out var managedByEmployeeId) ? managedByEmployeeId : null
            };

            bool success = await _apiService.AddSalesOffice(newSalesOffice);
            if (success)
            {
                await DisplayAlert("Success", "Sales Office added successfully.", "OK");
                GroupedEmployees.Clear();
                GroupedProperties.Clear();
                SalesOffices.Add(newSalesOffice);
            }
            else
            {
                await DisplayAlert("Error", "Failed to add Sales Office.", "OK");
            }
        }


        private async void OnEditSalesOfficeClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var salesOffice = button.CommandParameter as SalesOffice;
            if (salesOffice == null)
            {
                await DisplayAlert("Error", "Sales Office not found.", "OK");
                return;
            }

            string officeName = await DisplayPromptAsync("Edit Sales Office", "Enter Office Name:", initialValue: salesOffice.OfficeName);
            string addressIdStr = await DisplayPromptAsync("Edit Sales Office", "Enter Address ID:", initialValue: salesOffice.AddressID.ToString());
            string managedByEmployeeIdStr = await DisplayPromptAsync("Edit Sales Office", "Enter Managed By Employee ID:", initialValue: salesOffice.ManagedByEmployeeID.ToString());

            if (!int.TryParse(addressIdStr, out var addressId))
            {
                await DisplayAlert("Error", "Invalid Address ID.", "OK");
                return;
            }

            if (!int.TryParse(managedByEmployeeIdStr, out int managedByEmployeeId))
            {
                await DisplayAlert("Error", "Invalid Employee ID.", "OK");
                return;
            }

            salesOffice.OfficeName = officeName;
            salesOffice.AddressID = addressId;
            salesOffice.ManagedByEmployeeID = managedByEmployeeId;

            bool success = await _apiService.UpdateSalesOffice(salesOffice);
            if (success)
            {
                await DisplayAlert("Success", "Sales Office updated successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update Sales Office.", "OK");
            }
        }

        private async void OnDeleteSalesOfficeClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var salesOffice = button.CommandParameter as SalesOffice;
            if (salesOffice == null)
            {
                await DisplayAlert("Error", "Sales Office not found.", "OK");
                return;
            }

            bool success = await _apiService.DeleteSalesOffice(salesOffice.OfficeID);
            if (success)
            {
                SalesOffices.Remove(salesOffice);
                await DisplayAlert("Success", "Sales Office deleted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete Sales Office.", "OK");
            }
        }

        private async void OnGetSalesOfficeByIdClicked(object sender, EventArgs e)
        {
            if (int.TryParse(GetSalesOfficeIdEntry.Text, out var salesOfficeId))
            {
                try
                {
                    var salesOffice = await _apiService.GetSalesOffice(salesOfficeId);
                    SalesOffices.Clear();
                    GroupedProperties.Clear();

                    if (salesOffice != null)
                    {
                        SalesOffices.Add(salesOffice);
                    }
                }
                catch
                {
                    await DisplayAlert("Error", "SalesOffice not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid SalesOffice ID.", "OK");
            }
        }

        private async void OnShowPropertiesPerOfficeClicked(object sender, EventArgs e)
        {
            SalesOffices.Clear();
            GroupedProperties.Clear();
            GroupedEmployees.Clear();

            var salesOffices = await _apiService.GetAllSalesOffices();

            foreach (var office in salesOffices)
            {
                var properties = await _apiService.GetPropertiesByOfficeID(office.OfficeID);

                if (properties.Any())
                {
                    var groupedData = new SalesOfficeProperties()
                    {
                        OfficeName = office.OfficeName,
                        OfficeID = office.OfficeID,
                        Properties = new ObservableCollection<Property>(properties)
                    };

                    GroupedProperties.Add(groupedData);
                }
            }

            if (!GroupedProperties.Any())
            {
                await DisplayAlert("Info", "No properties found for any office.", "OK");
            }
        }

        private async void OnShowEmployeesPerOfficeClicked(object sender, EventArgs e)
        {
            SalesOffices.Clear();
            GroupedProperties.Clear();
            GroupedEmployees.Clear();

            var salesOffices = await _apiService.GetAllSalesOffices();

            foreach (var office in salesOffices)
            {
                var employees = await _apiService.GetEmployeesByOfficeID(office.OfficeID);

                if (employees.Any())
                {
                    var groupedData = new SalesOfficeEmployees()
                    {
                        OfficeName = office.OfficeName,
                        OfficeID = office.OfficeID,
                        Employees = new ObservableCollection<Employee>(employees)
                    };

                    GroupedEmployees.Add(groupedData);
                }
            }

            if (!GroupedEmployees.Any())
            {
                await DisplayAlert("Info", "No employees found for any office.", "OK");
            }
        }
    }
}

