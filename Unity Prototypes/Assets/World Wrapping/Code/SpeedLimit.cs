using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour {

    public float MaxSpeed = 200f;

    void FixedUpdate()
    {
        Rigidbody myBody = GetComponent<Rigidbody>();
        Debug.Log(myBody.velocity.magnitude);
        if (myBody.velocity.magnitude > MaxSpeed)
        {
            myBody.velocity = myBody.velocity.normalized * MaxSpeed;
        }
    }
}
