using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Projectile : MonoBehaviour
    {
        // Outlet
        private Rigidbody2D _rigidbody2D;
        
        // Methods
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = transform.right * 10f;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Target>())
            {
                SoundManager.instance.PlaySoundHit();
            } else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                SoundManager.instance.PlaySoundMiss();
            }
            Destroy(gameObject);
        }
        
        
    }
}

