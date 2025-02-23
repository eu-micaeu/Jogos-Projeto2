using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCamera : MonoBehaviour
{
    private Quaternion rotIniX;
    public float velRotX;
    private float countX = 0;
    void Start()
    {

        velRotX = 20;
        rotIniX = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;

    }


    void Update()
    {
        countX += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * velRotX;

        countX = Mathf.Clamp(countX, -60, 60);

        Quaternion rotX = Quaternion.AngleAxis(countX, Vector3.left);
        transform.localRotation = rotIniX * rotX;
    }
}
