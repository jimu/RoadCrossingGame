using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] AudioClip sfxDeath;
    [SerializeField] Vector3 dest;
    Vector3 start;

    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);// transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position == dest)
        {
            dest = start;
            start = transform.position;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0);
        }
    }
}
