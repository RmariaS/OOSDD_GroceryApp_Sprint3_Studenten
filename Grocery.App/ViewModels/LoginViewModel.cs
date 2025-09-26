using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Microsoft.Maui.Graphics;

namespace Grocery.App.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IClientRepository _clientRepository;
        private readonly GlobalViewModel _global;

        // Login properties
        [ObservableProperty] private string loginEmail = "";
        [ObservableProperty] private string loginPassword = "";

        // Registratie properties
        [ObservableProperty] private string registerName = "";
        [ObservableProperty] private string registerEmail = "";
        [ObservableProperty] private string registerPassword = "";

        // Feedback
        [ObservableProperty] private string loginMessage = "";
        [ObservableProperty] private Color messageColor = Colors.Red;

        public LoginViewModel(IAuthService authService, IClientRepository clientRepository, GlobalViewModel global)
        {
            _authService = authService;
            _clientRepository = clientRepository;
            _global = global;
        }

        [RelayCommand]
        private void Login()
        {
            var authenticatedClient = _authService.Login(LoginEmail, LoginPassword);
            if (authenticatedClient != null)
            {
                LoginMessage = $"Welkom {authenticatedClient.Name}!";
                MessageColor = Colors.Green;
                _global.Client = authenticatedClient;
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                LoginMessage = "Ongeldige inloggegevens.";
                MessageColor = Colors.Red;
            }
        }

        [RelayCommand]
        private void Register()
        {
            //  lege velden controle
            if (string.IsNullOrWhiteSpace(RegisterName) ||
                string.IsNullOrWhiteSpace(RegisterEmail) ||
                string.IsNullOrWhiteSpace(RegisterPassword))
            {
                LoginMessage = "Vul alle registratievelden in.";
                MessageColor = Colors.Red;
                return;
            }

            // Controleer wachtwoord
            if (!IsValidPassword(RegisterPassword))
            {
                LoginMessage = "Wachtwoord moet minimaal 8 tekens bevatten, inclusief een hoofdletter en een cijfer.";
                MessageColor = Colors.Red;
                return;
            }

            // Controle of mail al bestaat
            var allClients = _clientRepository.GetAll();
            if (allClients.Any(c => c.EmailAddress.Equals(RegisterEmail, StringComparison.OrdinalIgnoreCase)))
            {
                LoginMessage = "Dit e-mailadres is al geregistreerd.";
                MessageColor = Colors.Red;
                return;
            }

            // Nieuwe client aanmaken
            var newId = allClients.Count > 0 ? allClients.Max(c => c.Id) + 1 : 1;
            var newClient = new Client(newId, RegisterName, RegisterEmail, RegisterPassword);

            _clientRepository.AddClient(newClient);
            LoginMessage = $"Account succesvol aangemaakt voor {RegisterName}!";
            MessageColor = Colors.Green;

            //  meteen inloggen
            _global.Client = newClient;
            Application.Current.MainPage = new AppShell();
        }

        //  voor wachtwoordvalidatie
        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpper = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsDigit(c)) hasDigit = true;
            }

            return hasUpper && hasDigit;
        }
    }
}
