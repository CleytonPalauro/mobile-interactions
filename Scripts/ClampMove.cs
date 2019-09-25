using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampMove : MonoBehaviour
{
    public Vector3 myAcc;

    [Range(1f, 10f)]
    public float speed = 5f;

    private float down = 0f;

    [Range(0f, 1f)]
    public float downSpeed = 0.5f;

    void Start() { }

    void Update()
    {
        Vector3 accel = Input.acceleration;

        down = downSpeed * Time.deltaTime;

        Vector3 temp = transform.position;

        transform.Translate(new Vector3(
            accel.x * speed * Time.deltaTime, 0f, 0f));

        temp.z += speed * Time.deltaTime;

        temp.x = Mathf.Clamp(transform.position.x, -4f, 4f);

        transform.position = temp;
    }
}
