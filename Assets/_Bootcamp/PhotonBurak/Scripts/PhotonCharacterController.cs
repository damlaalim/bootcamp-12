using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PhotonCharacterController : MonoBehaviour
{
   private PlayerInput _playerInput;

   private InputAction moveAction;

   [SerializeField] private float speed = 5f;

   public GameObject Mark;

   public GameObject CanvasName;
   public Text Name;
   
   
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
      
   }


   void MovePlayer()
   {
       Vector2 direction = moveAction.ReadValue<Vector2>();
       transform.position += new Vector3(direction.x, 0, direction.y)* speed* Time.deltaTime;
   }
   
   
   
   
   
   
   
   
   
}