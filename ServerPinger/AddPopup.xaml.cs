using System.Windows;

namespace ServerPinger;

public partial class AddPopup : Window
{
    public AddPopup()
    {
        InitializeComponent();
    }
    
    public AddPopup(Server server)
    {
        InitializeComponent();
        NameTextBox.Text = server.Name;
        IpAddressTextBox.Text = server.IpAddress;
        DescriptionTextBox.Text = server.Description;
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}