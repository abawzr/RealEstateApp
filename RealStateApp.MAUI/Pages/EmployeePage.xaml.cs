using RealStateApp.MAUI.Models;
using RealStateApp.MAUI.Services;
using System.Collections.ObjectModel;

namespace RealStateApp.MAUI.Pages
{
    public partial class EmployeePage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Employee> Employees { get; set; }

        public EmployeePage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Employees = new ObservableCollection<Employee>();
            BindingContext = this;
        }

        private async void OnLoadEmployeesClicked(object sender, EventArgs e)
        {
            Employees.Clear();
            var employees = await _apiService.GetAllEmployees();

            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }

        private async void OnAddEmployeeClicked(object sender, EventArgs e)
        {
            var newEmployee = new Employee
            {
                EmpID = int.TryParse(EmpIdEntry.Text, out var empId) ? empId : 0,
                EmpFirstName = EmpFirstNameEntry.Text,
                EmpLastName = EmpLastNameEntry.Text,
                SalesOfficeID = int.TryParse(SalesOfficeIdEntry.Text, out var salesOfficeId) ? salesOfficeId : null,
                DateOfBirth = DateTime.TryParse(DateOfBirthEntry.Text, out var dateOfBirth) ? dateOfBirth : null,
                Age = int.TryParse(AgeEntry.Text, out var age) ? age : null
            };

            bool success = await _apiService.AddEmployee(newEmployee);
            if (success)
            {
                await DisplayAlert("Success", "Employee added successfully.", "OK");
                Employees.Add(newEmployee);
            }
            else
            {
                await DisplayAlert("Error", "Failed to add Employee.", "OK");
            }
        }


        private async void OnEditEmployeeClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var employee = button.CommandParameter as Employee;
            if (employee == null)
            {
                await DisplayAlert("Error", "Employee not found.", "OK");
                return;
            }

            string empFirstName = await DisplayPromptAsync("Edit Employee", "Enter First Name:", initialValue: employee.EmpFirstName);
            string empLastName = await DisplayPromptAsync("Edit Employee", "Enter Last Name:", initialValue: employee.EmpLastName);
            string salesOfficeIdStr = await DisplayPromptAsync("Edit Employee", "Enter Sales Office ID:", initialValue: employee.SalesOfficeID.ToString());
            string dateOfBirthStr = await DisplayPromptAsync("Edit Employee", "Enter Date Of Birth:", initialValue: employee.DateOfBirth.ToString());
            string ageStr = await DisplayPromptAsync("Edit Employee", "Enter Age:", initialValue: employee.Age.ToString());


            if (!int.TryParse(salesOfficeIdStr, out var salesOfficeId))
            {
                await DisplayAlert("Error", "Invalid Sales Office ID.", "OK");
                return;
            }

            if (!DateTime.TryParse(dateOfBirthStr, out var dateOfBirth))
            {
                await DisplayAlert("Error", "Invalid Date Of Birth.", "OK");
                return;
            }

            if (!int.TryParse(ageStr, out var age))
            {
                await DisplayAlert("Error", "Invalid Age.", "OK");
                return;
            }

            employee.EmpFirstName = empFirstName;
            employee.EmpLastName = empLastName;
            employee.SalesOfficeID = salesOfficeId;
            employee.DateOfBirth = dateOfBirth;
            employee.Age = age;

            bool success = await _apiService.UpdateEmployee(employee);
            if (success)
            {
                await DisplayAlert("Success", "Employee updated successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update Employee.", "OK");
            }
        }

        private async void OnDeleteEmployeeClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var employee = button.CommandParameter as Employee;
            if (employee == null)
            {
                await DisplayAlert("Error", "Employee not found.", "OK");
                return;
            }

            bool success = await _apiService.DeleteEmployee(employee.EmpID);
            if (success)
            {
                Employees.Remove(employee);
                await DisplayAlert("Success", "Employee deleted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete Employee.", "OK");
            }
        }

        private async void OnGetEmployeeByIdClicked(object sender, EventArgs e)
        {
            if (int.TryParse(GetEmployeeIdEntry.Text, out var empId))
            {
                try
                {
                    var employee = await _apiService.GetEmployee(empId);
                    Employees.Clear();

                    if (employee != null)
                    {
                        Employees.Add(employee);
                    }
                }
                catch
                {
                    await DisplayAlert("Error", "Employee not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid Employee ID.", "OK");
            }
        }
    }
}
