﻿using NETworkManager.Models.Settings;
using NETworkManager.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using NETworkManager.Models.PowerShell;
using static NETworkManager.Models.PuTTY.PuTTY;
// ReSharper disable InconsistentNaming

namespace NETworkManager.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        #region Variables
        private readonly bool _isLoading;

        public ICollectionView ProfileViews { get; }

        #region General
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name)
                    return;

                _name = value;
                OnPropertyChanged();
            }
        }

        private string _host;
        public string Host
        {
            get => _host;
            set
            {
                if (value == _host)
                    return;

                _host = value;
                OnPropertyChanged();
            }
        }

        private Guid _credentialID;
        public Guid CredentialID
        {
            get => _credentialID;
            set
            {
                if (value == _credentialID)
                    return;

                _credentialID = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView Credentials { get; }

        private string _group;
        public string Group
        {
            get => _group;
            set
            {
                if (value == _group)
                    return;

                _group = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView Groups { get; }

        private string _tags;
        public string Tags
        {
            get => _tags;
            set
            {
                if (value == _tags)
                    return;

                _tags = value;
                OnPropertyChanged();
            }
        }

        private bool _showUnlockCredentialsHint;
        public bool ShowUnlockCredentialsHint
        {
            get => _showUnlockCredentialsHint;
            set
            {
                if (value == _showUnlockCredentialsHint)
                    return;

                _showUnlockCredentialsHint = value;
                OnPropertyChanged();
            }
        }

        private bool _isEdited;
        public bool IsEdited
        {
            get => _isEdited;
            set
            {
                if (value == _isEdited)
                    return;

                _isEdited = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Network Interface
        private bool _networkInterface_Enabled;
        public bool NetworkInterface_Enabled
        {
            get => _networkInterface_Enabled;
            set
            {
                if (value == _networkInterface_Enabled)
                    return;

                _networkInterface_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _networkInterface_EnableDynamicIPAddress = true;
        public bool NetworkInterface_EnableDynamicIPAddress
        {
            get => _networkInterface_EnableDynamicIPAddress;
            set
            {
                if (value == _networkInterface_EnableDynamicIPAddress)
                    return;

                _networkInterface_EnableDynamicIPAddress = value;
                OnPropertyChanged();
            }
        }

        private bool _networkInterface_EnableStaticIPAddress;
        public bool NetworkInterface_EnableStaticIPAddress
        {
            get => _networkInterface_EnableStaticIPAddress;
            set
            {
                if (value == _networkInterface_EnableStaticIPAddress)
                    return;

                if (value)
                    NetworkInterface_EnableStaticDNS = true;

                _networkInterface_EnableStaticIPAddress = value;
                OnPropertyChanged();
            }
        }

        private string _networkInterface_IPAddress;
        public string NetworkInterface_IPAddress
        {
            get => _networkInterface_IPAddress;
            set
            {
                if (value == _networkInterface_IPAddress)
                    return;

                _networkInterface_IPAddress = value;
                OnPropertyChanged();
            }
        }

        private string _networkInterface_SubnetmaskOrCidr;
        public string NetworkInterface_SubnetmaskOrCidr
        {
            get => _networkInterface_SubnetmaskOrCidr;
            set
            {
                if (value == _networkInterface_SubnetmaskOrCidr)
                    return;

                _networkInterface_SubnetmaskOrCidr = value;
                OnPropertyChanged();
            }
        }

        private string _networkInterface_Gateway;
        public string NetworkInterface_Gateway
        {
            get => _networkInterface_Gateway;
            set
            {
                if (value == _networkInterface_Gateway)
                    return;

                _networkInterface_Gateway = value;
                OnPropertyChanged();
            }
        }

        private bool _networkInterface_EnableDynamicDNS = true;
        public bool NetworkInterface_EnableDynamicDNS
        {
            get => _networkInterface_EnableDynamicDNS;
            set
            {
                if (value == _networkInterface_EnableDynamicDNS)
                    return;

                _networkInterface_EnableDynamicDNS = value;
                OnPropertyChanged();
            }
        }

        private bool _networkInterface_EnableStaticDNS;
        public bool NetworkInterface_EnableStaticDNS
        {
            get => _networkInterface_EnableStaticDNS;
            set
            {
                if (value == _networkInterface_EnableStaticDNS)
                    return;

                _networkInterface_EnableStaticDNS = value;
                OnPropertyChanged();
            }
        }

        private string _networkInterface_PrimaryDNSServer;
        public string NetworkInterface_PrimaryDNSServer
        {
            get => _networkInterface_PrimaryDNSServer;
            set
            {
                if (value == _networkInterface_PrimaryDNSServer)
                    return;

                _networkInterface_PrimaryDNSServer = value;
                OnPropertyChanged();
            }
        }

        private string _networkInterface_SecondaryDNSServer;
        public string NetworkInterface_SecondaryDNSServer
        {
            get => _networkInterface_SecondaryDNSServer;
            set
            {
                if (value == _networkInterface_SecondaryDNSServer)
                    return;

                _networkInterface_SecondaryDNSServer = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IP Scanner

        private bool _ipScanner_Enabled;
        public bool IPScanner_Enabled
        {
            get => _ipScanner_Enabled;
            set
            {
                if (value == _ipScanner_Enabled)
                    return;

                _ipScanner_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _ipScanner_InheritHost;
        public bool IPScanner_InheritHost
        {
            get => _ipScanner_InheritHost;
            set
            {
                if (value == _ipScanner_InheritHost)
                    return;

                _ipScanner_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _ipScanner_IPRange;
        public string IPScanner_IPRange
        {
            get => _ipScanner_IPRange;
            set
            {
                if (value == _ipScanner_IPRange)
                    return;

                _ipScanner_IPRange = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Port Scanner

        private bool _portScanner_Enabled;
        public bool PortScanner_Enabled
        {
            get => _portScanner_Enabled;
            set
            {
                if (value == _portScanner_Enabled)
                    return;

                _portScanner_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _portScanner_InheritHost;
        public bool PortScanner_InheritHost
        {
            get => _portScanner_InheritHost;
            set
            {
                if (value == _portScanner_InheritHost)
                    return;

                _portScanner_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _portScanner_Host;
        public string PortScanner_Host
        {
            get => _portScanner_Host;
            set
            {
                if (value == _portScanner_Host)
                    return;

                _portScanner_Host = value;
                OnPropertyChanged();
            }
        }

        private string _portScanner_Ports;
        public string PortScanner_Ports
        {
            get => _portScanner_Ports;
            set
            {
                if (value == _portScanner_Ports)
                    return;

                _portScanner_Ports = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Ping
        private bool _ping_Enabled;
        public bool Ping_Enabled
        {
            get => _ping_Enabled;
            set
            {
                if (value == _ping_Enabled)
                    return;

                _ping_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _ping_InheritHost;
        public bool Ping_InheritHost
        {
            get => _ping_InheritHost;
            set
            {
                if (value == _ping_InheritHost)
                    return;

                _ping_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _ping_Host;
        public string Ping_Host
        {
            get => _ping_Host;
            set
            {
                if (value == _ping_Host)
                    return;

                _ping_Host = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Traceroute
        private bool _traceroute_Enabled;
        public bool Traceroute_Enabled
        {
            get => _traceroute_Enabled;
            set
            {
                if (value == _traceroute_Enabled)
                    return;

                _traceroute_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _traceroute_InheritHost;
        public bool Traceroute_InheritHost
        {
            get => _traceroute_InheritHost;
            set
            {
                if (value == _traceroute_InheritHost)
                    return;

                _traceroute_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _traceroute_Host;
        public string Traceroute_Host
        {
            get => _traceroute_Host;
            set
            {
                if (value == _traceroute_Host)
                    return;

                _traceroute_Host = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region DNS Lookup
        private bool _dnsLookup_Enabled;
        public bool DNSLookup_Enabled
        {
            get => _dnsLookup_Enabled;
            set
            {
                if (value == _dnsLookup_Enabled)
                    return;

                _dnsLookup_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _dnsLookup_InheritHost;
        public bool DNSLookup_InheritHost
        {
            get => _dnsLookup_InheritHost;
            set
            {
                if (value == _dnsLookup_InheritHost)
                    return;

                _dnsLookup_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _dnsLookup_Host;
        public string DNSLookup_Host
        {
            get => _dnsLookup_Host;
            set
            {
                if (value == _dnsLookup_Host)
                    return;

                _dnsLookup_Host = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region RemoteDesktop
        private bool _remoteDesktop_Enabled;
        public bool RemoteDesktop_Enabled
        {
            get => _remoteDesktop_Enabled;
            set
            {
                if (value == _remoteDesktop_Enabled)
                    return;

                _remoteDesktop_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _remoteDesktop_InheritHost;
        public bool RemoteDesktop_InheritHost
        {
            get => _remoteDesktop_InheritHost;
            set
            {
                if (value == _remoteDesktop_InheritHost)
                    return;

                _remoteDesktop_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _remoteDesktop_Host;
        public string RemoteDesktop_Host
        {
            get => _remoteDesktop_Host;
            set
            {
                if (value == _remoteDesktop_Host)
                    return;

                _remoteDesktop_Host = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PowerShell
        private bool _powerShell_Enabled;
        public bool PowerShell_Enabled
        {
            get => _powerShell_Enabled;
            set
            {
                if (value == _powerShell_Enabled)
                    return;

                _powerShell_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _powerShell_EnableRemoteConsole;
        public bool PowerShell_EnableRemoteConsole
        {
            get => _powerShell_EnableRemoteConsole;
            set
            {
                if (value == _powerShell_EnableRemoteConsole)
                    return;

                _powerShell_EnableRemoteConsole = value;
                OnPropertyChanged();
            }
        }

        private bool _powerShell_InheritHost;
        public bool PowerShell_InheritHost
        {
            get => _powerShell_InheritHost;
            set
            {
                if (value == _powerShell_InheritHost)
                    return;

                _powerShell_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _powerShell_Host;
        public string PowerShell_Host
        {
            get => _powerShell_Host;
            set
            {
                if (value == _powerShell_Host)
                    return;

                _powerShell_Host = value;
                OnPropertyChanged();
            }
        }

        private bool _powerShell_OverrideAdditionalCommandLine;
        public bool PowerShell_OverrideAdditionalCommandLine
        {
            get => _powerShell_OverrideAdditionalCommandLine;
            set
            {
                if (value == _powerShell_OverrideAdditionalCommandLine)
                    return;

                _powerShell_OverrideAdditionalCommandLine = value;
                OnPropertyChanged();
            }
        }

        private string _powerShell_AdditionalCommandLine;
        public string PowerShell_AdditionalCommandLine
        {
            get => _powerShell_AdditionalCommandLine;
            set
            {
                if (value == _powerShell_AdditionalCommandLine)
                    return;

                _powerShell_AdditionalCommandLine = value;
                OnPropertyChanged();
            }
        }

        private List<PowerShell.ExecutionPolicy> _powerShell_ExecutionPolicies = new List<PowerShell.ExecutionPolicy>();
        public List<PowerShell.ExecutionPolicy> PowerShell_ExecutionPolicies
        {
            get => _powerShell_ExecutionPolicies;
            set
            {
                if (value == _powerShell_ExecutionPolicies)
                    return;

                _powerShell_ExecutionPolicies = value;
                OnPropertyChanged();
            }
        }

        private bool _powerShell_OverrideExecutionPolicy;
        public bool PowerShell_OverrideExecutionPolicy
        {
            get => _powerShell_OverrideExecutionPolicy;
            set
            {
                if (value == _powerShell_OverrideExecutionPolicy)
                    return;

                _powerShell_OverrideExecutionPolicy = value;
                OnPropertyChanged();
            }
        }

        private PowerShell.ExecutionPolicy _powerShell_ExecutionPolicy;
        public PowerShell.ExecutionPolicy PowerShell_ExecutionPolicy
        {
            get => _powerShell_ExecutionPolicy;
            set
            {
                if (value == _powerShell_ExecutionPolicy)
                    return;

                _powerShell_ExecutionPolicy = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PuTTY 
        private bool _puTTY_Enabled;
        public bool PuTTY_Enabled
        {
            get => _puTTY_Enabled;
            set
            {
                if (value == _puTTY_Enabled)
                    return;

                _puTTY_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _puTTY_InheritHost;
        public bool PuTTY_InheritHost
        {
            get => _puTTY_InheritHost;
            set
            {
                if (value == _puTTY_InheritHost)
                    return;

                _puTTY_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_UseSSH; // Default is SSH
        public bool PuTTY_UseSSH
        {
            get => _puTTY_UseSSH;
            set
            {
                if (value == _puTTY_UseSSH)
                    return;

                if (value)
                {
                    if (PuTTY_ConnectionMode == ConnectionMode.Serial)
                        PuTTY_HostOrSerialLine = Host;

                    PuTTY_PortOrBaud = SettingsManager.Current.PuTTY_DefaultSSHPort;
                    PuTTY_ConnectionMode = ConnectionMode.SSH;
                }

                _puTTY_UseSSH = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_UseTelnet;
        public bool PuTTY_UseTelnet
        {
            get => _puTTY_UseTelnet;
            set
            {
                if (value == _puTTY_UseTelnet)
                    return;

                if (value)
                {
                    if (PuTTY_ConnectionMode == ConnectionMode.Serial)
                        PuTTY_HostOrSerialLine = Host;
                    
                    PuTTY_PortOrBaud = SettingsManager.Current.PuTTY_DefaultTelnetPort;
                    PuTTY_ConnectionMode = ConnectionMode.Telnet;
                }

                _puTTY_UseTelnet = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_UseSerial;
        public bool PuTTY_UseSerial
        {
            get => _puTTY_UseSerial;
            set
            {
                if (value == _puTTY_UseSerial)
                    return;

                if (value)
                {
                    if (PuTTY_ConnectionMode != ConnectionMode.Serial)
                        PuTTY_HostOrSerialLine = SettingsManager.Current.PuTTY_DefaultSerialLine;
                    
                    PuTTY_PortOrBaud = SettingsManager.Current.PuTTY_DefaultBaudRate;
                    PuTTY_ConnectionMode = ConnectionMode.Serial;
                }

                _puTTY_UseSerial = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_UseRlogin;
        public bool PuTTY_UseRlogin
        {
            get => _puTTY_UseRlogin;
            set
            {
                if (value == _puTTY_UseRlogin)
                    return;

                if (value)
                {
                    if (PuTTY_ConnectionMode == ConnectionMode.Serial)
                        PuTTY_HostOrSerialLine = Host;

                    PuTTY_PortOrBaud = SettingsManager.Current.PuTTY_DefaultRloginPort;
                    PuTTY_ConnectionMode = ConnectionMode.Rlogin;
                }

                _puTTY_UseRlogin = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_UseRAW;
        public bool PuTTY_UseRAW
        {
            get => _puTTY_UseRAW;
            set
            {
                if (value == _puTTY_UseRAW)
                    return;

                if (value)
                {
                    if (PuTTY_ConnectionMode == ConnectionMode.Serial)
                        PuTTY_HostOrSerialLine = Host;

                    PuTTY_PortOrBaud = 0;
                    PuTTY_ConnectionMode = ConnectionMode.RAW;
                }

                _puTTY_UseRAW = value;
                OnPropertyChanged();
            }
        }

        private string _puTTY_HostOrSerialLine;
        public string PuTTY_HostOrSerialLine
        {
            get => _puTTY_HostOrSerialLine;
            set
            {
                if (value == _puTTY_HostOrSerialLine)
                    return;

                _puTTY_HostOrSerialLine = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_OverridePortOrBaud;
        public bool PuTTY_OverridePortOrBaud
        {
            get => _puTTY_OverridePortOrBaud;
            set
            {
                if (value == _puTTY_OverridePortOrBaud)
                    return;

                _puTTY_OverridePortOrBaud = value;
                OnPropertyChanged();
            }
        }

        private int _puTTY_PortOrBaud;
        public int PuTTY_PortOrBaud
        {
            get => _puTTY_PortOrBaud;
            set
            {
                if (value == _puTTY_PortOrBaud)
                    return;

                _puTTY_PortOrBaud = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_OverrideUsername;
        public bool PuTTY_OverrideUsername
        {
            get => _puTTY_OverrideUsername;
            set
            {
                if (value == _puTTY_OverrideUsername)
                    return;

                _puTTY_OverrideUsername = value;
                OnPropertyChanged();
            }
        }

        private string _puTTY__Username;
        public string PuTTY_Username
        {
            get => _puTTY__Username;
            set
            {
                if (value == _puTTY__Username)
                    return;

                _puTTY__Username = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_OverrideProfile;
        public bool PuTTY_OverrideProfile
        {
            get => _puTTY_OverrideProfile;
            set
            {
                if (value == _puTTY_OverrideProfile)
                    return;

                _puTTY_OverrideProfile = value;
                OnPropertyChanged();
            }
        }

        private string _puTTY_Profile;
        public string PuTTY_Profile
        {
            get => _puTTY_Profile;
            set
            {
                if (value == _puTTY_Profile)
                    return;

                _puTTY_Profile = value;
                OnPropertyChanged();
            }
        }

        private bool _puTTY_OverrideAdditionalCommandLine;
        public bool PuTTY_OverrideAdditionalCommandLine
        {
            get => _puTTY_OverrideAdditionalCommandLine;
            set
            {
                if (value == _puTTY_OverrideAdditionalCommandLine)
                    return;

                _puTTY_OverrideAdditionalCommandLine = value;
                OnPropertyChanged();
            }
        }

        private string _puTTY_AdditionalCommandLine;
        public string PuTTY_AdditionalCommandLine
        {
            get => _puTTY_AdditionalCommandLine;
            set
            {
                if (value == _puTTY_AdditionalCommandLine)
                    return;

                _puTTY_AdditionalCommandLine = value;
                OnPropertyChanged();
            }
        }

        private ConnectionMode _puTTY_ConnectionMode;
        public ConnectionMode PuTTY_ConnectionMode
        {
            get => _puTTY_ConnectionMode;
            set
            {
                if (value == _puTTY_ConnectionMode)
                    return;

                _puTTY_ConnectionMode = value;
            }
        }
        #endregion

        #region TightVNC
        private bool _tightVNC_Enabled;
        public bool TightVNC_Enabled
        {
            get => _tightVNC_Enabled;
            set
            {
                if (value == _tightVNC_Enabled)
                    return;

                _tightVNC_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _tightVNC_InheritHost;
        public bool TightVNC_InheritHost
        {
            get => _tightVNC_InheritHost;
            set
            {
                if (value == _tightVNC_InheritHost)
                    return;

                _tightVNC_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _tightVNC_Host;
        public string TightVNC_Host
        {
            get => _tightVNC_Host;
            set
            {
                if (value == _tightVNC_Host)
                    return;

                _tightVNC_Host = value;
                OnPropertyChanged();
            }
        }


        private bool _tightVNC_OverridePort;
        public bool TightVNC_OverridePort
        {
            get => _tightVNC_OverridePort;
            set
            {
                if (value == _tightVNC_OverridePort)
                    return;

                _tightVNC_OverridePort = value;
                OnPropertyChanged();
            }
        }

        private int _tightVNC_Port;
        public int TightVNC_Port
        {
            get => _tightVNC_Port;
            set
            {
                if (value == _tightVNC_Port)
                    return;

                _tightVNC_Port = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Wake on LAN
        private bool _wakeOnLAN_Enabled;
        public bool WakeOnLAN_Enabled
        {
            get => _wakeOnLAN_Enabled;
            set
            {
                if (value == _wakeOnLAN_Enabled)
                    return;

                _wakeOnLAN_Enabled = value;

                OnPropertyChanged();
            }
        }

        private string _wakeOnLAN_MACAddress;
        public string WakeOnLAN_MACAddress
        {
            get => _wakeOnLAN_MACAddress;
            set
            {
                if (value == _wakeOnLAN_MACAddress)
                    return;

                _wakeOnLAN_MACAddress = value;
                OnPropertyChanged();
            }
        }

        private string _wakeOnLAN_Broadcast;
        public string WakeOnLAN_Broadcast
        {
            get => _wakeOnLAN_Broadcast;
            set
            {
                if (value == _wakeOnLAN_Broadcast)
                    return;

                _wakeOnLAN_Broadcast = value;
                OnPropertyChanged();
            }
        }

        private bool _wakeOnLAN_OverridePort;
        public bool WakeOnLAN_OverridePort
        {
            get => _wakeOnLAN_OverridePort;
            set
            {
                if (value == _wakeOnLAN_OverridePort)
                    return;

                _wakeOnLAN_OverridePort = value;
                OnPropertyChanged();
            }
        }

        private int _wakeOnLAN_Port;
        public int WakeOnLAN_Port
        {
            get => _wakeOnLAN_Port;
            set
            {
                if (value == _wakeOnLAN_Port)
                    return;

                _wakeOnLAN_Port = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HTTP Headers
        private bool _httpHeaders_Enabled;
        public bool HTTPHeaders_Enabled
        {
            get => _httpHeaders_Enabled;
            set
            {
                if (value == _httpHeaders_Enabled)
                    return;

                _httpHeaders_Enabled = value;

                OnPropertyChanged();
            }
        }

        private string _httpHeaders_Website;
        public string HTTPHeaders_Website
        {
            get => _httpHeaders_Website;
            set
            {
                if (value == _httpHeaders_Website)
                    return;

                _httpHeaders_Website = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Whois
        private bool _whois_Enabled;
        public bool Whois_Enabled
        {
            get => _whois_Enabled;
            set
            {
                if (value == _whois_Enabled)
                    return;

                _whois_Enabled = value;

                OnPropertyChanged();
            }
        }

        private bool _whois_InheritHost;
        public bool Whois_InheritHost
        {
            get => _whois_InheritHost;
            set
            {
                if (value == _whois_InheritHost)
                    return;

                _whois_InheritHost = value;
                OnPropertyChanged();
            }
        }

        private string _whois_Domain;
        public string Whois_Domain
        {
            get => _whois_Domain;
            set
            {
                if (value == _whois_Domain)
                    return;

                _whois_Domain = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #endregion

        public ProfileViewModel(Action<ProfileViewModel> saveCommand, Action<ProfileViewModel> cancelHandler, IReadOnlyCollection<string> groups, bool isEdited = false, ProfileInfo profileInfo = null)
        {
            _isLoading = true;

            // Load the view
            ProfileViews = new CollectionViewSource { Source = ProfileViewManager.List }.View;
            ProfileViews.SortDescriptions.Add(new SortDescription(nameof(ProfileViewInfo.Name), ListSortDirection.Ascending));

            SaveCommand = new RelayCommand(p => saveCommand(this));
            CancelCommand = new RelayCommand(p => cancelHandler(this));

            IsEdited = isEdited;

            var profileInfo2 = profileInfo ?? new ProfileInfo();

            Name = profileInfo2.Name;
            Host = profileInfo2.Host;

            if (CredentialManager.IsLoaded)
            {
                Credentials = CollectionViewSource.GetDefaultView(CredentialManager.CredentialInfoList);
            }
            else
            {
                ShowUnlockCredentialsHint = true;

                Credentials = profileInfo2.CredentialID == Guid.Empty ? new CollectionViewSource { Source = new List<CredentialInfo>() }.View : new CollectionViewSource { Source = new List<CredentialInfo> { new CredentialInfo(profileInfo2.CredentialID) } }.View;
            }

            CredentialID = profileInfo2.CredentialID;

            Group = string.IsNullOrEmpty(profileInfo2.Group) ? (groups.Count > 0 ? groups.OrderBy(x => x).First() : Resources.Localization.Strings.Default) : profileInfo2.Group;
            Tags = profileInfo2.Tags;

            Groups = CollectionViewSource.GetDefaultView(groups);
            Groups.SortDescriptions.Add(new SortDescription());

            // Network Interface
            NetworkInterface_Enabled = profileInfo2.NetworkInterface_Enabled;
            NetworkInterface_EnableDynamicIPAddress = !profileInfo2.NetworkInterface_EnableStaticIPAddress;
            NetworkInterface_EnableStaticIPAddress = profileInfo2.NetworkInterface_EnableStaticIPAddress;
            NetworkInterface_IPAddress = profileInfo2.NetworkInterface_IPAddress;
            NetworkInterface_Gateway = profileInfo2.NetworkInterface_Gateway;
            NetworkInterface_SubnetmaskOrCidr = profileInfo2.NetworkInterface_SubnetmaskOrCidr;
            NetworkInterface_EnableDynamicDNS = !profileInfo2.NetworkInterface_EnableStaticDNS;
            NetworkInterface_EnableStaticDNS = profileInfo2.NetworkInterface_EnableStaticDNS;
            NetworkInterface_PrimaryDNSServer = profileInfo2.NetworkInterface_PrimaryDNSServer;
            NetworkInterface_SecondaryDNSServer = profileInfo2.NetworkInterface_SecondaryDNSServer;

            // IP Scanner
            IPScanner_Enabled = profileInfo2.IPScanner_Enabled;
            IPScanner_InheritHost = profileInfo2.IPScanner_InheritHost;
            IPScanner_IPRange = profileInfo2.IPScanner_IPRange;

            // Port Scanner
            PortScanner_Enabled = profileInfo2.PortScanner_Enabled;
            PortScanner_InheritHost = profileInfo2.PortScanner_InheritHost;
            PortScanner_Host = profileInfo2.PortScanner_Host;
            PortScanner_Ports = profileInfo2.PortScanner_Ports;

            // Ping
            Ping_Enabled = profileInfo2.Ping_Enabled;
            Ping_InheritHost = profileInfo2.Ping_InheritHost;
            Ping_Host = profileInfo2.Ping_Host;

            // Traceroute
            Traceroute_Enabled = profileInfo2.Traceroute_Enabled;
            Traceroute_InheritHost = profileInfo2.Traceroute_InheritHost;
            Traceroute_Host = profileInfo2.Traceroute_Host;

            // DNS Lookup
            DNSLookup_Enabled = profileInfo2.DNSLookup_Enabled;
            DNSLookup_InheritHost = profileInfo2.DNSLookup_InheritHost;
            DNSLookup_Host = profileInfo2.DNSLookup_Host;

            // Remote Desktop
            RemoteDesktop_Enabled = profileInfo2.RemoteDesktop_Enabled;
            RemoteDesktop_InheritHost = profileInfo2.RemoteDesktop_InheritHost;
            RemoteDesktop_Host = profileInfo2.RemoteDesktop_Host;

            // PowerShell
            PowerShell_Enabled = profileInfo2.PowerShell_Enabled;
            PowerShell_EnableRemoteConsole = profileInfo2.PowerShell_EnableRemoteConsole;
            PowerShell_InheritHost = profileInfo2.PowerShell_InheritHost;
            PowerShell_Host = profileInfo2.PowerShell_Host;
            PowerShell_OverrideAdditionalCommandLine = profileInfo2.PowerShell_OverrideAdditionalCommandLine;
            PowerShell_AdditionalCommandLine = profileInfo2.PowerShell_AdditionalCommandLine;
            PowerShell_ExecutionPolicies = Enum.GetValues(typeof(PowerShell.ExecutionPolicy)).Cast<PowerShell.ExecutionPolicy>().ToList();
            PowerShell_OverrideExecutionPolicy = profileInfo2.PowerShell_OverrideExecutionPolicy;
            PowerShell_ExecutionPolicy = IsEdited ? profileInfo2.PowerShell_ExecutionPolicy : PowerShell_ExecutionPolicies.FirstOrDefault(x => x == SettingsManager.Current.PowerShell_DefaultExecutionPolicy); ;

            // PuTTY
            PuTTY_Enabled = profileInfo2.PuTTY_Enabled;

            switch (profileInfo2.PuTTY_ConnectionMode)
            {
                // SSH is default
                case ConnectionMode.SSH:
                    PuTTY_UseSSH = true;
                    break;
                case ConnectionMode.Telnet:
                    PuTTY_UseTelnet = true;
                    break;
                case ConnectionMode.Serial:
                    PuTTY_UseSerial = true;
                    break;
                case ConnectionMode.Rlogin:
                    PuTTY_UseRlogin = true;
                    break;
                case ConnectionMode.RAW:
                    PuTTY_UseRAW = true;
                    break;
            }

            PuTTY_InheritHost = profileInfo2.PuTTY_InheritHost;
            PuTTY_HostOrSerialLine = profileInfo2.PuTTY_HostOrSerialLine;
            PuTTY_OverridePortOrBaud = profileInfo2.PuTTY_OverridePortOrBaud;
            PuTTY_PortOrBaud = profileInfo2.PuTTY_OverridePortOrBaud ? profileInfo2.PuTTY_PortOrBaud : GetPortOrBaudByConnectionMode(PuTTY_ConnectionMode);
            PuTTY_OverrideUsername = profileInfo2.PuTTY_OverrideUsername;
            PuTTY_Username = profileInfo2.PuTTY_Username;
            PuTTY_OverrideProfile = profileInfo2.PuTTY_OverrideProfile;
            PuTTY_Profile = profileInfo2.PuTTY_Profile;
            PuTTY_OverrideAdditionalCommandLine = profileInfo2.PuTTY_OverrideAdditionalCommandLine;
            PuTTY_AdditionalCommandLine = profileInfo2.PuTTY_AdditionalCommandLine;

            // TightVNC
            TightVNC_Enabled = profileInfo2.TightVNC_Enabled;
            TightVNC_InheritHost = profileInfo2.TightVNC_InheritHost;
            TightVNC_Host = profileInfo2.TightVNC_Host;
            TightVNC_OverridePort = profileInfo2.TightVNC_OverridePort;
            TightVNC_Port = profileInfo2.TightVNC_OverridePort ? profileInfo2.TightVNC_Port : SettingsManager.Current.TightVNC_DefaultVNCPort;

            // Wake on LAN
            WakeOnLAN_Enabled = profileInfo2.WakeOnLAN_Enabled;
            WakeOnLAN_MACAddress = profileInfo2.WakeOnLAN_MACAddress;
            WakeOnLAN_Broadcast = profileInfo2.WakeOnLAN_Broadcast;
            WakeOnLAN_OverridePort = profileInfo2.WakeOnLAN_OverridePort;
            WakeOnLAN_Port = profileInfo2.WakeOnLAN_OverridePort ? profileInfo2.WakeOnLAN_Port : SettingsManager.Current.DefaultWakeOnLAN_Port;

            // HTTP Headers
            HTTPHeaders_Enabled = profileInfo2.HTTPHeaders_Enabled;
            HTTPHeaders_Website = profileInfo2.HTTPHeaders_Website;

            // Whois
            Whois_Enabled = profileInfo2.Whois_Enabled;
            Whois_InheritHost = profileInfo2.Whois_InheritHost;
            Whois_Domain = profileInfo2.Whois_Domain;

            _isLoading = false;
        }

        #region ICommands & Actions
        public ICommand SaveCommand { get; }

        public ICommand CancelCommand { get; }

        public ICommand UnselectCredentialCommand
        {
            get { return new RelayCommand(p => UnselectCredentialAction()); }
        }

        private void UnselectCredentialAction()
        {
            CredentialID = Guid.Empty;
        }
        #endregion
    }
}