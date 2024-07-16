using System.Linq;
using System.Threading.Tasks;
using DialogueEditor;
using GameFramework.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using WebSocketSharp;

namespace _Bootcamp.Scripts.LobbyCode.GameFramework.Manager
{
    public class RelayManager : Singleton<RelayManager>
    {


        private bool _isHost;
        private string _joinCode;
        private string _ip;
        private int _port;
        private byte[] _key;
        private byte[] _connectionData;
        private byte[] _hostconnectionData;

        private System.Guid _allocationId;
        private byte[] _allocationIdBytes;

        public bool IsHost
        {
            get { return _isHost; }

        }


        public string GetAllocationId()
        {
            return _allocationId.ToString();
        }
        public string GetConnectionData()
        {
            return _connectionData.ToString();
        }
        
        public async Task<string> CreateRelay(int maxConnection)
        {

            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
            _joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);


            RelayServerEndpoint dtlsEndpoint = allocation.ServerEndpoints.First(endpoint => endpoint.ConnectionType == "dtls");

            _ip = dtlsEndpoint.Host;
            _port = dtlsEndpoint.Port;

            _allocationId = allocation.AllocationId;
            _allocationIdBytes = allocation.AllocationIdBytes;
            _connectionData = allocation.ConnectionData;
            _key = allocation.Key;

            _isHost = true;
            return _joinCode;
        }

        public async Task<bool> JoinRelay(string joinCode)
        {
            _joinCode = joinCode;
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            
            RelayServerEndpoint dtlsEndpoint = allocation.ServerEndpoints.First(endpoint => endpoint.ConnectionType == "dtls");
            
            _ip = dtlsEndpoint.Host;
            _port = dtlsEndpoint.Port;

            _allocationId = allocation.AllocationId;
            _allocationIdBytes = allocation.AllocationIdBytes;
            _connectionData = allocation.ConnectionData;
            _hostconnectionData = allocation.HostConnectionData;
            _key = allocation.Key;

            return true;
        }
        public (byte[] AllocationId, byte[] Key, byte[] ConnectionData, string _dtlsAddress, int _dtlsPort) GetHostConnectionInfo()
        {
            return (_allocationIdBytes, _key, _connectionData, _ip, _port);
        }

        public (byte[] AllocationId, byte[] Key, byte[] ConnectionData, byte[] HostConnectionData, string _dtlsAddress, int _dtlsPort) GetClientConnectionInfo()
        {
            return (_allocationIdBytes, _key, _connectionData, _hostconnectionData, _ip, _port);
        }

        


       
    }
}