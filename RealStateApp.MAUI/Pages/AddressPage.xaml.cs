using RealStateApp.MAUI.Models;
using RealStateApp.MAUI.Services;
using System.Collections.ObjectModel;

namespace RealStateApp.MAUI.Pages
{
    public partial class AddressPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Address> Addresses { get; set; }

        public AddressPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Addresses = new ObservableCollection<Address>();
            BindingContext = this;
        }

        private async void OnLoadAddressesClicked(object sender, EventArgs e)
        {
            Addresses.Clear();
            var addresses = await _apiService.GetAllAddresses();

            foreach (var address in addresses)
            {
                Addresses.Add(address);
            }
        }

        private async void OnAddAddressClicked(object sender, EventArgs e)
        {
            var newAddress = new Address
            {
                AddressID = int.TryParse(AddressIdEntry.Text, out var empId) ? empId : 0,
                AddressLine = AddressLineEntry.Text,
                City = CityEntry.Text,
                State = StateEntry.Text,
                ZipCode = int.TryParse(ZipCodeEntry.Text, out var zipCode) ? zipCode : null,
            };

            bool success = await _apiService.AddAddress(newAddress);
            if (success)
            {
                await DisplayAlert("Success", "Address added successfully.", "OK");
                Addresses.Add(newAddress);
            }
            else
            {
                await DisplayAlert("Error", "Failed to add Address.", "OK");
            }
        }


        private async void OnEditAddressClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var address = button.CommandParameter as Address;
            if (address == null)
            {
                await DisplayAlert("Error", "Address not found.", "OK");
                return;
            }

            string addressLine = await DisplayPromptAsync("Edit Address", "Enter Address Line:", initialValue: address.AddressLine);
            string city = await DisplayPromptAsync("Edit Address", "Enter City:", initialValue: address.City);
            string state = await DisplayPromptAsync("Edit Address", "Enter State:", initialValue: address.State);
            string zipCodeStr = await DisplayPromptAsync("Edit Address", "Enter ZipCode:", initialValue: address.ZipCode.ToString());


            if (!int.TryParse(zipCodeStr, out var zipCode))
            {
                await DisplayAlert("Error", "Invalid ZipCode.", "OK");
                return;
            }

            address.AddressLine = addressLine;
            address.City = city;
            address.State = state;
            address.ZipCode = zipCode;

            bool success = await _apiService.UpdateAddress(address);
            if (success)
            {
                await DisplayAlert("Success", "Address updated successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update Address.", "OK");
            }
        }

        private async void OnDeleteAddressClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var address = button.CommandParameter as Address;
            if (address == null)
            {
                await DisplayAlert("Error", "Address not found.", "OK");
                return;
            }

            bool success = await _apiService.DeleteAddress(address.AddressID);
            if (success)
            {
                Addresses.Remove(address);
                await DisplayAlert("Success", "Address deleted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete Address.", "OK");
            }
        }

        private async void OnGetAddressByIdClicked(object sender, EventArgs e)
        {
            if (int.TryParse(GetAddressIdEntry.Text, out var addressId))
            {
                try
                {
                    var address = await _apiService.GetAddress(addressId);
                    Addresses.Clear();

                    if (address != null)
                    {
                        Addresses.Add(address);
                    }
                }
                catch
                {
                    await DisplayAlert("Error", "Address not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid Address ID.", "OK");
            }
        }
    }
}