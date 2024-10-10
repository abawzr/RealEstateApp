using RealStateApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.MAUI.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7081/")
            };
        }

        // SalesOffice

        public async Task<List<SalesOffice>> GetAllSalesOffices()
        {
            return await _httpClient.GetFromJsonAsync<List<SalesOffice>>("api/SalesOffice");
        }

        public async Task<SalesOffice> GetSalesOffice(int officeID)
        {
            return await _httpClient.GetFromJsonAsync<SalesOffice>($"api/salesoffice/{officeID}");
        }

        public async Task<bool> AddSalesOffice(SalesOffice salesOffice)
        {
            var response = await _httpClient.PostAsJsonAsync("api/SalesOffice", salesOffice);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateSalesOffice(SalesOffice salesOffice)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/salesoffice/{salesOffice.OfficeID}", salesOffice);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSalesOffice(int officeID)
        {
            var response = await _httpClient.DeleteAsync($"api/SalesOffice/{officeID}");
            return response.IsSuccessStatusCode;
        }

        // Employee

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>("api/Employee");
        }

        public async Task<Employee> GetEmployee(int empID)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"api/Employee/{empID}");
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Employee", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Employee/{employee.EmpID}", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEmployee(int empID)
        {
            var response = await _httpClient.DeleteAsync($"api/Employee/{empID}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Employee>> GetEmployeesByOfficeID(int salesOfficeId)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/Employee/ByOffice/{salesOfficeId}");
        }

        // Address

        public async Task<List<Address>> GetAllAddresses()
        {
            return await _httpClient.GetFromJsonAsync<List<Address>>("api/Address");
        }

        public async Task<Address> GetAddress(int addressID)
        {
            return await _httpClient.GetFromJsonAsync<Address>($"api/Address/{addressID}");
        }

        public async Task<bool> AddAddress(Address address)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Address", address);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAddress(Address address)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Address/{address.AddressID}", address);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAddress(int addressID)
        {
            var response = await _httpClient.DeleteAsync($"api/Address/{addressID}");
            return response.IsSuccessStatusCode;
        }

        // Owner

        public async Task<List<Owner>> GetAllOwners()
        {
            return await _httpClient.GetFromJsonAsync<List<Owner>>("api/Owner");
        }

        public async Task<Owner> GetOwner(int ownerID)
        {
            return await _httpClient.GetFromJsonAsync<Owner>($"api/Owner/{ownerID}");
        }

        public async Task<bool> AddOwner(Owner owner)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Owner", owner);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateOwner(Owner owner)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Owner/{owner.OwnerID}", owner);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOwner(int ownerID)
        {
            var response = await _httpClient.DeleteAsync($"api/Owner/ {ownerID}");
            return response.IsSuccessStatusCode;
        }

        // Property

        public async Task<List<Property>> GetAllProperties()
        {
            return await _httpClient.GetFromJsonAsync<List<Property>>("api/Property");
        }

        public async Task<Property> GetProperty(int propertyID)
        {
            return await _httpClient.GetFromJsonAsync<Property>($"api/Property/{propertyID}");
        }

        public async Task<bool> AddProperty(Property property)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Property", property);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProperty(Property property)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Property/{property.PropertyID}", property);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProperty(int propertyID)
        {
            var response = await _httpClient.DeleteAsync($"api/Property/{propertyID}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Property>> GetPropertiesByOfficeID(int salesOfficeId)
        {
            return await _httpClient.GetFromJsonAsync<List<Property>>($"api/Property/ByOffice/{salesOfficeId}");
        }

        // User

        public async Task<List<User>> GetAllUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/User");
        }

        public async Task<User> GetUser(Guid userID)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/User/{userID}");
        }

        public async Task<bool> AddUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/User/{user.UserID}", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(Guid userID)
        {
            var response = await _httpClient.DeleteAsync($"api/User/{userID}");
            return response.IsSuccessStatusCode;
        }

        // PropOwner

        public async Task<List<PropOwnerTable>> GetAllPropsOwners()
        {
            return await _httpClient.GetFromJsonAsync<List<PropOwnerTable>>("api/PropOwnerTable");
        }

        public async Task<PropOwnerTable> GetPropOwner(int ownerID, int propertyID)
        {
            return await _httpClient.GetFromJsonAsync<PropOwnerTable>($"api/PropOwnerTable/{ownerID}/{propertyID}");
        }

        public async Task<bool> AddPropOwner(PropOwnerTable propOwner)
        {
            var response = await _httpClient.PostAsJsonAsync("api/PropOwnerTable", propOwner);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePropOwner(PropOwnerTable propOwner)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/PropOwnerTable/{propOwner.OwnerID}/{propOwner.PropertyID}", propOwner);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePropOwner(int ownerID, int propertyID)
        {
            var response = await _httpClient.DeleteAsync($"api/PropOwnerTable/{ownerID}/{propertyID}");
            return response.IsSuccessStatusCode;
        }
    }
}
