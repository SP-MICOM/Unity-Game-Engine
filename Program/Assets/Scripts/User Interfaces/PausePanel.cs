using Photon.Pun;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PausePanel : MonoBehaviourPunCallbacks
{
    public void Continue()
    {
        MouseManager.Instance.SetMouse(false);

        gameObject.SetActive(false);
    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        gameObject.SetActive(false);

        PhotonNetwork.LoadLevel("Lobby");
    }
}
