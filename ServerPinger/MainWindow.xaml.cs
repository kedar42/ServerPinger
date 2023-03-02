using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerPinger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Server> _servers;

        public MainWindow()
        {
            _servers = new ObservableCollection<Server>(
                new List<Server>
                {
                    new("Google", "www.google.com", "Google search engine"),
                    new("Microsoft", "www.microsoft.com", "Microsoft website"),
                    new("GitHub", "www.github.com", "GitHub website"),
                    new("Xamarin", "www.xamarin.com", "Xamarin website"),
                    new("Localhost", "localhost", "Localhost"),
                    new("IPLocalhost", "0.0.0.0", "Ip address of localhost")
                });
            InitializeComponent();
            ServerList.ItemsSource = _servers;
        }

        private async void PingServers(object sender, RoutedEventArgs e)
        {
            await Parallel.ForEachAsync(_servers, Ping);
        }

        private void AddToDatabase(object sender, RoutedEventArgs e)
        {
            var popup = new AddPopup();
            if (popup.ShowDialog() != true) return;

            var name = popup.NameTextBox.Text;
            var ip = popup.IpAddressTextBox.Text;
            var description = popup.DescriptionTextBox.Text;

            _servers.Add(new Server(name, ip, description));
        }

        private void RemoveFromDatabase(object sender, RoutedEventArgs e)
        {
            var selectedList = ServerList.SelectedItems.Cast<object>().ToList();
            if (selectedList.Count == 0) return;
            
            foreach (var selected in selectedList)
            {
                _servers.Remove((Server) selected);
            }
        }

        private void EditSelected(object sender, RoutedEventArgs e)
        {
            if (ServerList.SelectedItems.Count != 1) return;
            var selected = (Server) ServerList.SelectedItem;
            var popup = new AddPopup(selected);
            if (popup.ShowDialog() != true) return;
            
            var name = popup.NameTextBox.Text;
            var ip = popup.IpAddressTextBox.Text;
            var description = popup.DescriptionTextBox.Text;
            
            if (name == selected.Name && ip == selected.IpAddress && description == selected.Description) return;
            
            selected.Name = name;
            selected.IpAddress = ip;
            selected.Description = description;
            selected.Status = ServerStatus.Unknown;
            
            

        }

        private static async ValueTask Ping(Server server, CancellationToken cancellationToken)
        {
            try
            {
                var reply = await new Ping().SendPingAsync(server.IpAddress);

                // todo add latency and more statuses
                server.Status = reply.Status == IPStatus.Success ? ServerStatus.Online : ServerStatus.Offline;
            }
            // todo add more exception feedback
            catch (Exception)
            {
                server.Status = ServerStatus.Error;
            }
        }
    }
}