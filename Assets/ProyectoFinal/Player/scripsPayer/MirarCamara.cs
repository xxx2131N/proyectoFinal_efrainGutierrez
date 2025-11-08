using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarCamara : MonoBehaviour
{
    public float sensibilidad = 100f;
    public Transform Jugador;

    float RotacionX = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        RotacionX -= MouseY;
        RotacionX = Mathf.Clamp(RotacionX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(RotacionX, 0, 0);
        Jugador.Rotate(Vector3.up * MouseX);
    }
}
