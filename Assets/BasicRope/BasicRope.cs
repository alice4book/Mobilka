using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child);
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            rb.centerOfMass = new Vector3(0.0f,0.0f,0.0f);
            rb.inertia = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
