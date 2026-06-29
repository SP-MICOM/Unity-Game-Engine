using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float axis;
    [SerializeField] float speed;
    [SerializeField] float mouseX;
    [SerializeField] float mouseY;

    private void Update()
    {
        mouseX += Input.GetAxisRaw("Mouse X");
        mouseY += Input.GetAxisRaw("Mouse Y");
    }

    public void RotateX(float minAngle, float maxAngle)
    {
        transform.localEulerAngles = new Vector3();

        axis = Mathf.Clamp(axis, minAngle, maxAngle);
    }

    public void RotateY(Rigidbody rigidbody)
    {
        axis += mouseX * speed * Time.fixedDeltaTime;

        rigidbody.transform.eulerAngles = new Vector3(0, axis, 0);
    }
}
