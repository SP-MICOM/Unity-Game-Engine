using Photon.Pun;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


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
        rotation.mouseY = Input.GetAxisRaw("Mouse Y");

        rotation.RotateX(minimumAngle, maximumAngle);
    }
}
