using Photon.Pun;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Character : MonoBehaviourPun
{
    [SerializeField] Vector3 direction = new Vector3();
    [SerializeField] float speed;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Rotation rotation;
    [SerializeField] Animator animator;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rotation = GetComponent<Rotation>();
        animator = GetComponent<Animator>();
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

        if (direction.x > 0 || direction.z > 0)
        {
            animator.SetInteger("X", (int)direction.x);
            animator.SetInteger("Y", (int)direction.z);
        }

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
