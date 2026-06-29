using Photon.Pun;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Character : MonoBehaviourPun
{
    [SerializeField] Vector3 direction = new Vector3();
    [SerializeField] float speed;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Rotation rotation;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rotation = GetComponent<Rotation>();
    }

    private void Start()
    {
        DisableCamera();   
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Control();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Move();

            rotation.RotateY(rigidbody);
        }
    }

    void Control()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        direction.Normalize();

    }

    void Move()
    {
        rigidbody.MovePosition(rigidbody.position + rigidbody.transform.TransformDirection(direction) * speed * Time.fixedDeltaTime);
    }

    private void DisableCamera()
    {
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            Camera eyes = transform.GetComponentInChildren<Camera>();

            eyes.GetComponent<AudioListener>().gameObject.SetActive(false);

            eyes.gameObject.SetActive(false);
        }
    }
}
