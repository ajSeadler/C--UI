using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Text;
using System.IO;

namespace PasswordManagerUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnGenerateClick(object? sender, RoutedEventArgs e)
    {
        string account = AccountInput.Text ?? "";
        string email = EmailInput.Text ?? "";
        string lengthStr = LengthInput.Text ?? "";

        if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(email) || !int.TryParse(lengthStr, out int length) || length <= 0)
        {
            Output.Text = "Please enter valid info in all fields.";
            return;
        }

        string password = GeneratePassword(length);
        string path = "/Users/ajseadler/passwords.txt";

        try
        {
            string record = $"Account: {account}\nEmail: {email}\nPassword: {password}\n---\n";
            File.AppendAllText(path, record);
            Output.Text = $"Password generated:\n{password}\n\nSaved to:\n{path}";
        }
        catch (Exception ex)
        {
            Output.Text = $"Error saving file:\n{ex.Message}";
        }
    }

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
