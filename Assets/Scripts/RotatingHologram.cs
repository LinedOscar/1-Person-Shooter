using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingHologram : MonoBehaviour
{

    public float speed = 50.0f;
    public bool local = true;

    void Start()
    {
        


    }

    
    void FixedUpdate()
    {
        if(local == true)
        {

            transform.Rotate(Vector3.up * speed * Time.deltaTime);

        }
        
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);

    }
}
