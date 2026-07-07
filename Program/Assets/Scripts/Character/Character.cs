using Photon.Pun;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Character : MonoBehaviourPun, IPunObservable
{
    [SerializeField] Vector3 direction = new Vector3();
    [SerializeField] float speed;
    [SerializeField] float health = 100;
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

            rotation.RotateY(rigidbody);

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
        rotation.mouseX = Input.GetAxisRaw("Mouse X");

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
            PhotonView view = other.GetComponent<PhotonView>();

            if(view == null)
            {
                Debug.Log("Robot Object does not have a PhotonVeiw");
            }

            if (view.IsMine || PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            // 내 오브젝트라면 다른 클라이언트에게 데이터를 전송합니다.
            stream.SendNext(health);
        }
        else
        {
            // 다른 클라이언트에게 데이터를 받습니다.
            health = (float)stream.ReceiveNext();
        }
    }
}