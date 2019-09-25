using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensores : MonoBehaviour
{
    public Vector3 myRot;

    [Range(1f, 10f)]
    public float speed = 5f;

    private float yPos = 0f;

    [Range(0f, 1f)]
    public float fall = 0.5f;

    void Start() { }

    void Update()
    {
        //Debug.Log(Input.deviceOrientation);
        if(Input.deviceOrientation == DeviceOrientation.FaceDown)
        {
            Debug.Log("Celular com a tela para cima!");
        }

        Vector3 myAccel = Input.acceleration;

        yPos -= fall * Time.deltaTime;

        //transform.Rotate(new Vector3(accel.x, myRot.y, myRot.z));

        transform.Translate(new Vector3(
            myAccel.x * speed * Time.deltaTime,
            yPos,
            myAccel.z * speed * Time.deltaTime));
    }
}
