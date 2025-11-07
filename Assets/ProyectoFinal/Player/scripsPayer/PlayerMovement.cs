using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // rb.freezeRotation = true;
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 velocity = Vector3.zero;

        if (movimientoHorizontal != 0 || movimientoVertical != 0)
        {
            Vector3 direccion = (transform.forward * movimientoVertical + transform.right * movimientoHorizontal).normalized;
            velocity = direccion * speed;
        }

        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }
}
