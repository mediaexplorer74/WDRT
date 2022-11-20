using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
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

namespace Interop.SirepClient
{

    public class SirepClientClass
    {
        public Action<object, EventArgs> Disconnect;

        public int CreateProcess(string commandText, string argumentsText, string workingFolder, uint v)
        {
            throw new NotImplementedException();
        }

        public int GetSirepProtocolRevision()
        {
            throw new NotImplementedException();
        }

        public uint LaunchWithOutput(uint totalMilliseconds, 
            string v1, string text, string v2, uint v3, 
            Connectivity.CallbackHandler outputCallback)
        {
            throw new NotImplementedException();
        }


        public void SirepConnect(uint totalMilliseconds, bool v)
        {
            throw new NotImplementedException();
        }

        public void SirepCreateDirectory(string remoteDirectoryName)
        {
            throw new NotImplementedException();
        }

        public void SirepDirectoryEnum(string remoteDirectoryName, 
            Connectivity.CallbackHandler outputCallback)
        {
            throw new NotImplementedException();
        }

        public void SirepDisconnect()
        {
            throw new NotImplementedException();
        }

        public void SirepGetFile(uint totalMilliseconds,
            string remoteFileName, string localFileName, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public RemoteFileInfo SirepGetFileInfo(string remoteFileName)
        {
            throw new NotImplementedException();
        }

        public void SirepInitializeWithEndpoints(SirepEndpointInfo localEndpoint, 
            SirepEndpointInfo remoteEndpoint)
        {
            throw new NotImplementedException();
        }

        public bool SirepPing(uint totalMilliseconds)
        {
            throw new NotImplementedException();
        }

        public void SirepPutFile(uint totalMilliseconds, bool v, 
            string localFileName, string remoteFileName, bool overwrite)
        {
            throw new NotImplementedException();
        }

        public void SirepRemoveDirectory(string remoteDirectoryName)
        {
            throw new NotImplementedException();
        }

        public void SirepRemoveFile(string remoteFileName)
        {
            throw new NotImplementedException();
        }

        public void SirepSetSshPort(ushort sshPort)
        {
            throw new NotImplementedException();
        }

        public Connectivity.RemoteDevice.TransportProtocol SirepUsedProtocol()
        {
            throw new NotImplementedException();
        }

        public void SirepUser(string userName, SecureString securePassword)
        {
            throw new NotImplementedException();
        }
    }

    public class RemoteDevice
    {

        public TransportProtocol SirepUsedProtocol()
        {
            throw new NotImplementedException();
        }
    }

    public class TransportProtocol
    {
    }

    public class SirepEndpointInfo
    {
        public ushort usProtocol2Port;
        public int usEchoPort;
        public int usSirepPort;
        public string wszIPAddress;
    }

    public class SirepDiscovery
    {
        public Action<object, SirepDiscovery.DiscoveredEventArgs> Discovered;

        public SirepDiscovery()
        {
            //TODO
        }

        public void Start(Guid soughtGuid)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public class DiscoveredEventArgs
        {
            //
            public Connectivity.DiscoveredDeviceInfo.ConnectionType ConnectionType;
            public Guid Guid;
            public string Name;
            public string Location;
            public string Address;
            public string Architecture;
            public string OSVersion;
            public int SshPort;
            public int SirepPort;
        }
    }

    public class RemoteFileInfo
    {
        public DateTime LastWriteTime;
        public long FileSize;
    }

    public class Connectivity
    {
        public class CallbackHandler
        {
        }

        public class RemoteDevice
        {
            public class TransportProtocol
            { 
            }
        }

        public class DiscoveredDeviceInfo
        {
            public enum ConnectionType
            {
            }
        }
    }
}
