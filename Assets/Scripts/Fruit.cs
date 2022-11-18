using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceEffect;
    public int points = 1;
    public int endPointsz = 1;
    private void Awake()
    {
        fruitCollider = GetComponent<Collider>();
        fruitRigidbody = GetComponent<Rigidbody>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();
    }
    private void Slice(Vector3 direction , Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(points);
        whole.SetActive(false);
        sliced.SetActive(true);
        fruitCollider.enabled = false;
        juiceEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Deg2Rad;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce); 
        }

        if (!other.CompareTag("Bomb"))
        {
            if (other.CompareTag("Border"))
            {
                FindObjectOfType<GameManager>().IncreaseEndPoints(endPointsz);

                
            }
        }
        
    }
}
