using RealStateApp.MAUI.Models;
using RealStateApp.MAUI.Services;
using System.Collections.ObjectModel;

namespace RealStateApp.MAUI.Pages
{
    public partial class UserPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<User> Users { get; set; }

        public UserPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Users = new ObservableCollection<User>();
            BindingContext = this;
        }

        private async void OnLoadUsersClicked(object sender, EventArgs e)
        {
            Users.Clear();
            var users = await _apiService.GetAllUsers();

            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            var newUser = new User
            {
                UserEmail = UserEmailEntry.Text,
            };

            bool success = await _apiService.AddUser(newUser);
            if (success)
            {
                await DisplayAlert("Success", "User added successfully.", "OK");
                Users.Add(newUser);
            }
            else
            {
                await DisplayAlert("Error", "Failed to add User.", "OK");
            }
        }


        private async void OnEditUserClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var user = button.CommandParameter as User;
            if (user == null)
            {
                await DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            string userEmail = await DisplayPromptAsync("Edit User", "Enter User Email:", initialValue: user.UserEmail);

            user.UserEmail = userEmail;

            bool success = await _apiService.UpdateUser(user);
            if (success)
            {
                await DisplayAlert("Success", "User updated successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update User.", "OK");
            }
        }

        private async void OnDeleteUserClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var user = button.CommandParameter as User;
            if (user == null)
            {
                await DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            bool success = await _apiService.DeleteUser(user.UserID);
            if (success)
            {
                Users.Remove(user);
                await DisplayAlert("Success", "User deleted successfully.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete User.", "OK");
            }
        }

        private async void OnGetUserByIdClicked(object sender, EventArgs e)
        {
            if (Guid.TryParse(UserIdEntry.Text, out var userId))
            {
                try
                {
                    var user = await _apiService.GetUser(userId);
                    Users.Clear();

                    if (user != null)
                    {
                        Users.Add(user);
                    }
                }
                catch {
                    await DisplayAlert("Error", "User not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid User ID.", "OK");
            }
        }
    }
}
