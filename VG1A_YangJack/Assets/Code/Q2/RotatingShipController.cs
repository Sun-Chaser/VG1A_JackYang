using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q2
{
    public class RotatingShipController : MonoBehaviour
    {
        // Outlets 
        private Rigidbody2D _rb;
        
        // Configuration
        public float speed;
        public float rotateSpeed;
        
        // Methods
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            // Turn Left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rb.AddTorque(rotateSpeed * Time.deltaTime);
            }
            
            // Turn Right
            if (Input.GetKey(KeyCode.RightArrow)) 
            {
                _rb.AddTorque(-rotateSpeed * Time.deltaTime);
            }
            
            // Thrust Forward
            if (Input.GetKey(KeyCode.Space))
            {
                _rb.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
            }
        }
    }
}