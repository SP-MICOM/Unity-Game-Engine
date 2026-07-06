using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform parentTransform;

    private Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    public override void OnConnectedToMaster()
    {
        if(PhotonNetwork.InLobby == false)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void Open()
    {
        PanelManager.Instance.Open(Panel.Room);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        for(int i = 0; i < roomList.Count; i++)
        {
            // 룸이 삭제된 경우
            if (roomList[i].RemovedFromList)
            {
                dictionary.TryGetValue(roomList[i].Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(roomList[i].Name);
            }
            else
            {
                // 룸이 처음 생성되는 경우
                if(dictionary.TryGetValue(roomList[i].Name, out prefab) == false)
                {
                    prefab = Instantiate(Resources.Load<GameObject>("Room Button"), parentTransform);

                    dictionary.Add(roomList[i].Name, prefab);
                }

                prefab.GetComponent<RoomStatus>().Refresh(roomList[i], i);
            }
        }

    }
}
