using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float axis;
    [SerializeField] float speed;

    public float mouseX { set; get; }
    public float mouseY { set; get; }

    public void RotateX(float minAngle, float maxAngle)
    {
        axis += mouseY * speed * Time.deltaTime;

        axis = Mathf.Clamp(axis, minAngle, maxAngle);

        transform.localEulerAngles = new Vector3(-axis, 0, 0);
    }

    public void RotateY(Rigidbody rigidbody)
    {
        axis += mouseX * speed;

        rigidbody.transform.eulerAngles = new Vector3(0, axis, 0);
    }
}
