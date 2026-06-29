using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;

public class SubscribePanel : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField [ ] inputFields = new TMP_InputField[3];

    private void Awake()
    {
        inputFields = GetComponentsInChildren<TMP_InputField>();
    }

    public void Subscribe()
    {
        var request = new RegisterPlayFabUserRequest
        {      
            Username = inputFields[0].text,
            Email = inputFields[1].text,
            Password = inputFields[2].text,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, Success, Failed);
    }

    public void Success(RegisterPlayFabUserResult registerPlayFabUserResult)
    {
        gameObject.SetActive(false);
    }

    public void Failed(PlayFabError playFabError)
    {
        PanelManager.Instance.Open(Panel.Error, playFabError.GenerateErrorReport());
    }
}
