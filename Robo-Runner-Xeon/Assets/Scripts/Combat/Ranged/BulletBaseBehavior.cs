using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseBehavior : MonoBehaviour
{
    public float speed = 0.1f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("OnTriggerEnter2D called!");
        Destroy(gameObject);    
    }
}
