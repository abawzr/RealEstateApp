using RealStateApp.MAUI.Models;
using RealStateApp.MAUI.Services;
using System.Collections.ObjectModel;

namespace RealStateApp.MAUI.Pages
{
    public partial class PropertyPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Property> Properties { get; set; }

        public PropertyPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Properties = new ObservableCollection<Property>();
            BindingContext = this;
        }

        private async void OnLoadPropertiesClicked(object sender, EventArgs e)
        {
            Properties.Clear();
            var properties = await _apiService.GetAllProperties();

            foreach (var property in properties)
            {
                Properties.Add(property);
            }
        }

        private async void OnAddPropertyClicked(object sender, EventArgs e)
        {
            var newProperty = new Property
            {
                PropertyID = int.TryParse(PropertyIdEntry.Text, out var propertyId) ? propertyId : 0,
                ListPrice = int.TryParse(ListPriceEntry.Text, out var listPrice) ? listPrice : 0,
                Status = bool.TryParse(StatusEntry.Text, out var status) ? status : false,
                NoOfBedrooms = int.TryParse(NoOfBedroomsEntry.Text, out var noOfBedrooms) ? noOfBedrooms : 0,
                NoOfBathrooms = int.TryParse(NoOfBathroomsEntry.Text, out var noOfBathrooms) ? noOfBathrooms : 0,
                City = CityEntry.Text,
                SalesOfficeID = int.TryParse(SalesOfficeIdEntry.Text, out var salesOfficeId) ? salesOfficeId : 0,
            };

            bool success = await _apiService.AddProperty(newProperty);
            if (success)
            {
                await DisplayAlert("Success", "Property added successfully.", "OK");
                Properties.Add(newProperty);
            }
            else
            {
                await DisplayAlert("Error", "Failed to add Property.", "OK");
            }
        }


        private async void OnEditPropertyClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var property = button.CommandParameter as Property;
            if (property == null)
            {
                await DisplayAlert("Error", "Property not found.", "OK");
                return;
            }

            string listPriceStr = await DisplayPromptAsync("Edit Property", "Enter List Price:", initialValue: property.ListPrice.ToString());
            string statusStr = await DisplayPromptAsync("Edit Property", "Enter Status:", initialValue: property.Status.ToString());
            string noOfBedroomsStr = await DisplayPromptAsync("Edit Property", "Enter Number Of Bedrooms:", initialValue: property.NoOfBedrooms.ToString());
            string noOfBathroomsStr = await DisplayPromptAsync("Edit Property", "Enter Number Of Bathrooms:", initialValue: property.NoOfBathrooms.ToString());
            string city = await DisplayPromptAsync("Edit Property", "Enter City:", initialValue: property.City);
            string salesOfficeIdStr = await DisplayPromptAsync("Edit Property", "Enter SalesOffice ID:", initialValue: property.SalesOfficeID.ToString());

            if (!int.TryParse(listPriceStr, out var listPrice))
            {
                await DisplayAlert("Error", "Invalid List Price.", "OK");
                return;
            }
            
            if (!bool.TryParse(statusStr, out var status))
            {
                await DisplayAlert("Error", "Invalid Status.", "OK");
                return;
            }
            
            if (!int.TryParse(noOfBedroomsStr, out var noOfBedrooms))
            {
                await DisplayAlert("Error", "Invalid Number Of Bedrooms.", "OK");
                return;
            }

            if (!int.TryParse(noOfBathroomsStr, out var noOfBathrooms))
            {
                await DisplayAlert("Error", "Invalid Number Of Bathrooms.", "OK");
                return;
            }

            if (!int.TryParse(salesOfficeIdStr, out var salesOfficeId))
            {
                await DisplayAlert("Error", "Invalid SalesOffice ID.", "OK");
                return;
            }

            property.ListPrice = listPrice;
            property.Status = status;
            property.NoOfBedrooms = noOfBedrooms;
            property.NoOfBathrooms = noOfBathrooms;
            property.City = city;
            property.SalesOfficeID = salesOfficeId;

            bool success = await _apiService.UpdateProperty(property);
            if (success)
            {
                await DisplayAlert("Success", "Property updated successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update Property.", "OK");
            }
        }

        private async void OnDeletePropertyClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var property = button.CommandParameter as Property;
            if (property == null)
            {
                await DisplayAlert("Error", "Property not found.", "OK");
                return;
            }

            bool success = await _apiService.DeleteProperty(property.PropertyID);
            if (success)
            {
                Properties.Remove(property);
                await DisplayAlert("Success", "Property deleted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete Property.", "OK");
            }
        }

        private async void OnGetPropertyByIdClicked(object sender, EventArgs e)
        {
            if (int.TryParse(GetPropertyIdEntry.Text, out var propertyId))
            {
                try
                {
                    var property = await _apiService.GetProperty(propertyId);
                    Properties.Clear();

                    if (property != null)
                    {
                        Properties.Add(property);
                    }
                }
                catch
                {
                    await DisplayAlert("Error", "Property not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid Property ID.", "OK");
            }
        }
    }
}
