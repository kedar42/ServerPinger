using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ServerPinger;

public class Server : INotifyPropertyChanged
{
    
    private ServerStatus _status;
    private string _ipAddress;
    private string _name;
    private string _description;


    public string IpAddress
    {
        get => _ipAddress;
        set
        {
            if (value == _ipAddress) return;
            _ipAddress = value;
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (value == _description) return;
            _description = value;
            OnPropertyChanged();
        }
    }

    public ServerStatus Status
    {
        get => _status;
        set
        {
            if (value == _status) return;
            _status = value;
            OnPropertyChanged();
        }
    }

    public Color StatusColor
    {
        get
        {
            return Status switch
            {
                ServerStatus.Online => Colors.Green,
                ServerStatus.Offline => Colors.Red,
                ServerStatus.Unknown => Colors.Gray,
                ServerStatus.Error => Colors.Orange,
                ServerStatus.Pinging => Colors.Azure,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public Server()
    {
        _ipAddress = "";
        _name = "";
        _description = "";
        _status = ServerStatus.Unknown;
    }

    public Server(string name, string ipAddress, string description)
    {
        IpAddress = ipAddress;
        Name = name;
        Description = description;
        Status = ServerStatus.Unknown;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}