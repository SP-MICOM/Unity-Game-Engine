using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;


public class Head : MonoBehaviourPunCallbacks
{
    [SerializeField] Rotation rotation;

    [SerializeField] float minimumAngle = -55f;
    [SerializeField] float maximumAngle = 55f;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
    }

    private void Update()
    {
        rotation.RotateX(minimumAngle, maximumAngle);
    }
}
