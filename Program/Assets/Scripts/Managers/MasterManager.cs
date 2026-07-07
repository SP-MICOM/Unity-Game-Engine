using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MasterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform creatTransform;
    [SerializeField] GameObject clone;

    private IEnumerator Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            while (true)
            {
                if (PhotonNetwork.CurrentRoom != null && clone == null)
                {
                    clone = PhotonNetwork.InstantiateRoomObject("Robot", Vector3.zero, Quaternion.identity);

                    clone.transform.position = creatTransform.position;
                }

                yield return new WaitForSeconds(5.0f);
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }
}
