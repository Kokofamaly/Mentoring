using System;

namespace Task3;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnClicked(object sender, EventArgs e)
    {
        string username = UserInput.Text ?? "User";
        await DisplayAlert("Greeting", $"Hello, {username}!", "OK");
    }
}