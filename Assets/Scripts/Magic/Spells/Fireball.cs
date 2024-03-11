using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : BaseSpell
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
        
        // Rank and element
        _spellRank = SpellRanks.Intermediate;
        _element = Elements.Fire;
        
        //destroy after lifetime ends
        Destroy(this.gameObject, lifetime);
        Physics.IgnoreLayerCollision(6, 7);

        name = "Fireball";
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
        // apply spell effect to whatever we hit
        // particle effect of hit, sound, and damage
        Debug.Log(MagicManager.Instance.CalculateDamage(this, 1));
        Explode();
        Destroy(this.gameObject);
    }

    private void Explode()
    {
        
    }
}
