using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.IO;
using System.Text;

namespace PasswordManagerUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for the "Generate Password" button
        private void OnGenerateClick(object sender, RoutedEventArgs e)
        {
            // Ensure the text input fields are not null
            string account = AccountInput?.Text ?? string.Empty;  // Use fallback if null
            string email = EmailInput?.Text ?? string.Empty;      // Same for email
            string lengthText = LengthInput?.Text ?? string.Empty; // Same for length

            // Validate account and email
            if (string.IsNullOrWhiteSpace(account))
            {
                Output.Text = "Account name cannot be empty.";
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                Output.Text = "Email/username cannot be empty.";
                return;
            }

            // Get password length from input
            if (!int.TryParse(lengthText, out int length) || length <= 0)
            {
                Output.Text = "Invalid password length.";
                return;
            }

            // Generate password
            string password = GeneratePassword(length);
            Output.Text = $"\nGenerated Password: {password}";

            // Save password to file
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "passwords.txt");
            try
            {
                string record = $"Account: {account}\nEmail: {email}\nPassword: {password}\n---\n";
                File.AppendAllText(path, record);
                Output.Text += $"\nPassword info saved to: {path}";
            }
            catch (Exception ex)
            {
                Output.Text += $"\nError writing to file: {ex.Message}";
            }
        }

        // Generate a random password
        private string GeneratePassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[rnd.Next(chars.Length)]);
            }

            return sb.ToString();
        }
    }
}
