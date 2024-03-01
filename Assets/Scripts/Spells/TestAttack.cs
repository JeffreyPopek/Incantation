using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class TestAttack : MonoBehaviour
{
    public float baseDamage = 10f;
    public float manaCost = 10f;
    public int requiredLevelToCast = 1;
    public float speed = 10f;
    public float lifeline = 2f;
    public float radius = 0.5f;
    
    
    private SphereCollider collider;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = radius;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        
        //destroy after lifetime ends
        Destroy(this.gameObject, lifeline);
        Physics.IgnoreLayerCollision(6, 7);
    }

    private void Update()
    {
        if (speed > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // apply spell effect to whatever we hit
        // particle effect of hit, sound, and damage
        Destroy(this.gameObject);
    }
}
