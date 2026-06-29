using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using System;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] int personnel = 0;
    [SerializeField] Toggle [ ] toggles;
    [SerializeField] Button createRoomButton;
    [SerializeField] TMP_InputField roomNameInputField;

    private void Start()
    {
        Select();

        OnRoomNameChanged();
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = personnel;

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);

        gameObject.SetActive(false);
    }

    public void OnRoomNameChanged()
    {
        createRoomButton.interactable = string.IsNullOrWhiteSpace(roomNameInputField.text) == false;
    }

    public void Select()
    {
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                personnel = i + 2;

                break;
            }
        }
    }

}
