using Photon.Pun;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class Network_Manager : MonoBehaviourPunCallbacks
    {
        void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }
}