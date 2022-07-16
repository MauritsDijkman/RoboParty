using UnityEngine;
using Photon.Pun;

public class ConveyorObjectScript : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        //if (PhotonNetwork.IsMasterClient)
        // {
        if (other.CompareTag("Oven"))
        {
            Debug.Log("Collision is oven");

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Client is master!");
                //PhotonNetwork.Destroy(this.gameObject);
                Destroy(this.gameObject);
                Debug.Log("Object is destroyed!");
            }

            //Debug.Log("Collision: " + other.gameObject.transform.name);
            //PhotonNetwork.Destroy(this.gameObject);
            //photonView.RPC("DestroyScraps", RpcTarget.All, this.gameObject);
            //Debug.Log("Photonview called");
        }
        // }
    }

    [PunRPC]
    void DestroyScraps(GameObject destroyObject)
    {
        //if (PhotonNetwork.IsMasterClient)
        // {
        Debug.Log("Start of photon view");
        destroyObject.SetActive(false);
        Destroy(destroyObject);
        Debug.Log("Object is destroyed!");
        // }
    }
}
