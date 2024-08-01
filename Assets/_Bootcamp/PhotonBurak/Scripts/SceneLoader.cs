using Photon.Pun;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class SceneLoader : MonoBehaviourPunCallbacks
    {
        public void ChangeScene()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Office");
            }
        }
    }
}