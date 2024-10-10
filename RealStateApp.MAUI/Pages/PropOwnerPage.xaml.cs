using RealStateApp.MAUI.Models;
using RealStateApp.MAUI.Services;
using System.Collections.ObjectModel;

namespace RealStateApp.MAUI.Pages
{
    public partial class PropOwnerPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<PropOwnerTable> PropsOwners { get; set; }

        public PropOwnerPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            PropsOwners = new ObservableCollection<PropOwnerTable>();
            BindingContext = this;
        }

        private async void OnLoadPropsOwnersClicked(object sender, EventArgs e)
        {
            PropsOwners.Clear();
            var propsOwners = await _apiService.GetAllPropsOwners();

            foreach (var propOwner in propsOwners)
            {
                PropsOwners.Add(propOwner);
            }
        }

        private async void OnAddPropOwnerClicked(object sender, EventArgs e)
        {
            var newPropOwner = new PropOwnerTable
            {
                OwnerID = int.TryParse(OwnerIdEntry.Text, out var ownerId) ? ownerId : 0,
                PropertyID = int.TryParse(PropertyIdEntry.Text, out var propertyId) ? propertyId : 0,
                PercentOwned = decimal.TryParse(PercentOwnedEntry.Text, out var percentOwned) ? percentOwned : 0,
            };

            bool success = await _apiService.AddPropOwner(newPropOwner);
            if (success)
            {
                await DisplayAlert("Success", "PropOwner added successfully.", "OK");
                PropsOwners.Add(newPropOwner);
            }
            else
            {
                await DisplayAlert("Error", "Failed to add PropOwner.", "OK");
            }
        }


        private async void OnEditPropOwnerClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var propOwner = button.CommandParameter as PropOwnerTable;
            if (propOwner == null)
            {
                await DisplayAlert("Error", "PropOwner not found.", "OK");
                return;
            }

            string ownerIdStr = await DisplayPromptAsync("Edit PropOwner", "Enter Owner ID:", initialValue: propOwner.OwnerID.ToString());
            string propertyIdStr = await DisplayPromptAsync("Edit PropOwner", "Enter Property ID:", initialValue: propOwner.PropertyID.ToString());
            string percentOwnedStr = await DisplayPromptAsync("Edit PropOwner", "Enter Percent Owned:", initialValue: propOwner.PercentOwned.ToString());

            if (!int.TryParse(ownerIdStr, out var ownerId))
            {
                await DisplayAlert("Error", "Invalid Owner ID.", "OK");
                return;
            }

            if (!int.TryParse(propertyIdStr, out var propertyId))
            {
                await DisplayAlert("Error", "Invalid Property ID.", "OK");
                return;
            }

            if (!decimal.TryParse(percentOwnedStr, out var percentOwned))
            {
                await DisplayAlert("Error", "Invalid Percent Owned.", "OK");
                return;
            }

            propOwner.OwnerID = ownerId;
            propOwner.PropertyID = propertyId;
            propOwner.PercentOwned = percentOwned;

            bool success = await _apiService.UpdatePropOwner(propOwner);
            if (success)
            {
                await DisplayAlert("Success", "PropOwner updated successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update PropOwner.", "OK");
            }
        }

        private async void OnDeletePropOwnerClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var propOwner = button.CommandParameter as PropOwnerTable;
            if (propOwner == null)
            {
                await DisplayAlert("Error", "PropOwner not found.", "OK");
                return;
            }

            bool success = await _apiService.DeletePropOwner(propOwner.OwnerID, propOwner.PropertyID);
            if (success)
            {
                PropsOwners.Remove(propOwner);
                await DisplayAlert("Success", "PropOwner deleted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete PropOwner.", "OK");
            }
        }

        private async void OnGetPropOwnerByIdClicked(object sender, EventArgs e)
        {
            if (int.TryParse(GetOwnerIdEntry.Text, out var ownerId) && int.TryParse(GetPropertyIdEntry.Text, out var propertyId))
            {
                try
                {
                    var propOwner = await _apiService.GetPropOwner(ownerId, propertyId);
                    PropsOwners.Clear();

                    if (propOwner != null)
                    {
                        PropsOwners.Add(propOwner);
                    }
                }
                catch
                {
                    await DisplayAlert("Error", "PropOwner not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid Owner ID or Property ID.", "OK");
            }
        }
    }
}