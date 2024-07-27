using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PhotonCharacterController : MonoBehaviourPunCallbacks
{
   private PlayerInput _playerInput;

   private InputAction moveAction;

   [SerializeField] private float speed = 5f;

   public GameObject Mark;

   public GameObject CanvasName;
   public Text Name;
   
   private bool isNearCube = false;

   
   private void Start()
   {
       PhotonNetwork.SendRate = 60;
       PhotonNetwork.SerializationRate = 5;
       
      _playerInput = GetComponent<PlayerInput>();
      moveAction = _playerInput.actions.FindAction("Move");
      
       CanvasName.SetActive(true);
       Name.text = GetComponent<PhotonView>().Controller.NickName;
      
      
   }

   private void Update()
   {
       if (GetComponent<PhotonView>().IsMine)
       {
             MovePlayer();
       }
       
       if (isNearCube && Input.GetKeyDown(KeyCode.E))
       {
           Debug.Log("E tuşuna basıldı ve küpe yakın");
           GameObject lightObject = GameObject.Find("LightObjectName"); // Işık nesnesinin adını buraya yazın
           if (lightObject != null)
           {
               PhotonView photonView = lightObject.GetComponent<PhotonView>();
               if (photonView != null)
               {
                   Debug.Log("PhotonView bulundu ve RPC çağrısı yapılacak");
                   photonView.RPC("ChangeLightColor", RpcTarget.All);
               }
               else
               {
                   Debug.LogError("PhotonView bulunamadı");
               }
           }
           else
           {
               Debug.LogError("LightObjectName adında nesne bulunamadı");
           }
       }
      
   }


   void MovePlayer()
   {
       Vector2 direction = moveAction.ReadValue<Vector2>();
       transform.position += new Vector3(direction.x, 0, direction.y)* speed* Time.deltaTime;
   }
   
   
   private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.CompareTag("Cube"))
       {
           isNearCube = true;
           Debug.Log("triggerlandı");
       }
   }

   private void OnTriggerExit(Collider other)
   {
       if (other.gameObject.CompareTag("Cube"))
       {
           isNearCube = false;
       }
   }
   
}