using UnityEngine;
using Photon.Pun;

public class Switch : MonoBehaviourPunCallbacks
{
    public LightControl lightControl; // Bu switch'in kontrol ettiği ışık
    public LightSequenceManager sequenceManager; // Sıralama kontrolü
    private bool isPlayerNearby = false; // Oyuncunun yakın olup olmadığını kontrol et

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Switch'i açıp kapat
            ToggleSwitch();
            // Ağ üzerinden diğer oyunculara bildirim gönder
            photonView.RPC("RPC_ToggleSwitch", RpcTarget.All, lightControl.lightOrder);
            Debug.Log("Switch activated with order: " + lightControl.lightOrder);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Karakterin collider'ı ile çarpışmayı kontrol et
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Karakter uzaklaştığında
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    [PunRPC]
    void RPC_ToggleSwitch(int lightOrder)
    {
        // Switch'i açıp kapat
        ToggleSwitch();
        // Işığın sıralama bilgilerini ekle
        sequenceManager.AddToSequence(lightOrder);
        Debug.Log("Switch activated with order: " + lightOrder);

    }

    void ToggleSwitch()
    {
        bool newState = !lightControl.lightObject.enabled;
        lightControl.SetLightState(newState);
    }
}