using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Vector3 target;
    private float speed;
    public Action<MovingObject> OnDestroyed;

    public void Initialize(Vector3 target, float speed, Action<MovingObject> onDestroyed)
    {
        this.target = target;
        this.speed = speed;
        this.OnDestroyed = onDestroyed;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 pushDirection = transform.position - collision.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-pushDirection.normalized * 5f, ForceMode.Impulse);
        }
    }
}
