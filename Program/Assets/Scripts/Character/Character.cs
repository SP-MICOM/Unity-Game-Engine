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
            rotation.RotateY(rigidbody);

            Control();

            Animate();

            Pause();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Move();
        }
    }

    void Control()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        direction.Normalize();

    }

    void Animate()
    {
        animator.SetInteger("X", Mathf.Abs((int)direction.x));
        animator.SetInteger("Y", Mathf.Abs((int)direction.z));
    }

    void Move()
    {
        rigidbody.linearVelocity = rigidbody.transform.TransformDirection(direction).normalized * speed;
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

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PanelManager.Instance.Open(Panel.Pause);

            MouseManager.Instance.SetMouse(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Robot"))
        {
            PhotonNetwork.Destroy(other.gameObject);
        }
    }
}
