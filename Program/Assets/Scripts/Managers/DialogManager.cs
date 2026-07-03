using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;

public class DialogManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Transform parentTransform;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            inputField.ActivateInputField();

            if(inputField.text.Length <= 0)
            {
                return;
            }

            string message = $"<color=green>{PhotonNetwork.LocalPlayer.NickName} </color>" + " : " + inputField.text;

            // RpcTarget.All : 현재 룸에 있는 모든 클라이언트에게 Talk() 함수를
            // 실행하라는 명령을 전달합니다.

            photonView.RPC("Send", RpcTarget.All, message);

            inputField.text = "";

            inputField.ActivateInputField();
        }
    }

    [PunRPC]
    public void Send(string message)
    {
        Text talk = Instantiate(Resources.Load<Text>("Message"), parentTransform);

        talk.text = message;

        Canvas.ForceUpdateCanvases();

        scrollRect.verticalNormalizedPosition = 0f;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string message = $"<color=yellow>{newPlayer.NickName} joined the game.</color>";

        photonView.RPC("Send", RpcTarget.All, message);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        string message = $"<color=yellow>{otherPlayer.NickName} left the game.</color>";

        photonView.RPC("Send", RpcTarget.All, message);
    }
}
