using RealStateApp.MAUI.Models;
using RealStateApp.MAUI.Services;
using System.Collections.ObjectModel;

namespace RealStateApp.MAUI.Pages
{
    public partial class OwnerPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Owner> Owners { get; set; }

        public OwnerPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Owners = new ObservableCollection<Owner>();
            BindingContext = this;
        }

        private async void OnLoadOwnersClicked(object sender, EventArgs e)
        {
            Owners.Clear();
            var owners = await _apiService.GetAllOwners();

            foreach (var owner in owners)
            {
                Owners.Add(owner);
            }
        }

        private async void OnAddOwnerClicked(object sender, EventArgs e)
        {
            var newOwner = new Owner
            {
                OwnerID = int.TryParse(OwnerIdEntry.Text, out var ownerId) ? ownerId : 0,
                OwnerFirstName = FirstNameEntry.Text,
                OwnerLastName = LastNameEntry.Text,
            };

            bool success = await _apiService.AddOwner(newOwner);
            if (success)
            {
                await DisplayAlert("Success", "Owner added successfully.", "OK");
                Owners.Add(newOwner);
            }
            else
            {
                await DisplayAlert("Error", "Failed to add Owner.", "OK");
            }
        }


        private async void OnEditOwnerClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var owner = button.CommandParameter as Owner;
            if (owner == null)
            {
                await DisplayAlert("Error", "Owner not found.", "OK");
                return;
            }

            string firstName = await DisplayPromptAsync("Edit Owner", "Enter First Name:", initialValue: owner.OwnerFirstName);
            string lastName = await DisplayPromptAsync("Edit Owner", "Enter Last Name:", initialValue: owner.OwnerLastName);

            owner.OwnerFirstName = firstName;
            owner.OwnerLastName = lastName;

            bool success = await _apiService.UpdateOwner(owner);
            if (success)
            {
                await DisplayAlert("Success", "Owner updated successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update Owner.", "OK");
            }
        }

        private async void OnDeleteOwnerClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var owner = button.CommandParameter as Owner;
            if (owner == null)
            {
                await DisplayAlert("Error", "Owner not found.", "OK");
                return;
            }

            bool success = await _apiService.DeleteOwner(owner.OwnerID);
            if (success)
            {
                Owners.Remove(owner);
                await DisplayAlert("Success", "Owner deleted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete Owner.", "OK");
            }
        }

        private async void OnGetOwnerByIdClicked(object sender, EventArgs e)
        {
            if (int.TryParse(GetOwnerIdEntry.Text, out var ownerId))
            {
                try
                {
                    var owner = await _apiService.GetOwner(ownerId);
                    Owners.Clear();

                    if (owner != null)
                    {
                        Owners.Add(owner);
                    }
                }
                catch
                {
                    await DisplayAlert("Error", "Owner not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid Owner ID.", "OK");
            }
        }
    }
}
