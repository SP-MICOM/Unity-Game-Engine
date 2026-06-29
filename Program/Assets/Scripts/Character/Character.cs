using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Character : MonoBehaviourPun
{
    [SerializeField] Vector3 direction = new Vector3();

    private void Start()
    {
        DisableCamera();   
    }

    private void Update()
    {
        Control();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Control()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        direction.Normalize();

    }

    void Move()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.MovePosition(direction);
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
