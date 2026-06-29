using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> createPosition = new List<Transform>();

    private void Start()
    {
        Create();
    }

    public void Create()
    {
        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        PhotonNetwork.Instantiate("Character", createPosition[index].position, Quaternion.identity);
    }

}
