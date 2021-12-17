using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public float turnSpeed = 8;
    public float smoothingTime = 0.1f;

    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    Vector3 velocity;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * input.magnitude);
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, input.magnitude, ref smoothMoveVelocity, smoothingTime);
        ;
        velocity = transform.forward * speed * smoothInputMagnitude;
    }

    void FixedUpdate() {
        body.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        body.MovePosition(body.position + velocity * Time.deltaTime);
    }
}
