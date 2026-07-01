using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MasterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform creatTransform;

    private IEnumerator Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            while (true)
            {
                GameObject clone = PhotonNetwork.InstantiateRoomObject("Robot", Vector3.zero, Quaternion.identity);

                clone.transform.position = creatTransform.position;
                
                yield return new WaitForSeconds(5.0f);
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log(PhotonNetwork.PlayerList[0].NickName);

        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }
}
