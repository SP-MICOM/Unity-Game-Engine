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

             string message = $"<color=green>{PhotonNetwork.LocalPlayer.NickName} </color>" + " : " + inputField.text;

            Text talk = Instantiate(Resources.Load<Text>("Message"), parentTransform);

            talk.text = message;

            inputField.text = "";

            inputField.ActivateInputField();
        }

    }
}
