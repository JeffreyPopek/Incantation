using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SonicBlast : BaseSpell
{
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
        Destroy(this.gameObject, lifetime);
        Physics.IgnoreLayerCollision(6, 7);

        name = "SonicBlast";
        // set values in the editor
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
        Debug.Log(other.name);
        float knockbackRadius = 5f;
        float knockbackStrength = 5f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, knockbackRadius);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
        
            if (rb != null)
            {
                rb.AddExplosionForce(knockbackStrength, transform.position, knockbackRadius, 0f, ForceMode.Impulse);
            }
        }
        
        Destroy(this.gameObject);
    }

}
