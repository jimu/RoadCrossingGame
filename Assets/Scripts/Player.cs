using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    Vector3 currentPosition;
    Vector3 targetPosition;
    Vector3 goalPosition;
    float speed = 2f;
    float startTime;
    [SerializeField] float edgeDistance = 2f;
    FrameAnimation anim;

    AudioSource footsteps;

    private void Start()
    {
        currentPosition = transform.position;
        anim           = GetComponentInChildren<FrameAnimation>();
        footsteps      = GetComponent<AudioSource>();
        goalPosition   = GameObject.FindGameObjectWithTag("Goal").transform.position;
        targetPosition = transform.position;
    }

    public void Move(Vector3 displacement)
    {
        if (transform.position == targetPosition && Mathf.Abs(currentPosition.x + displacement.x) < edgeDistance)
        {
            currentPosition = transform.position;
            targetPosition += displacement;
            startTime = Time.time;
            if (displacement.x != 0)
                transform.localScale = new Vector3(displacement.x < 0 ? -1 : 1, 1, 1);
            //Debug.Log(string.Format("Move {0} from {1} to {2}", displacement, currentPosition, targetPosition));
        }
    }

    public void Update()
    {
        if (GameManager.instance.IsPlaying())
        {
            bool moving = transform.position != targetPosition;
            anim.enabled = moving;
            footsteps.mute = !moving;
            if (moving)
                transform.position = Vector3.Lerp(currentPosition, targetPosition, (Time.time - startTime) * speed);
            else
                currentPosition = targetPosition;

            if (currentPosition == goalPosition)
                GameManager.instance.Win();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(collision.GetComponent<Coin>().GetValue());
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            GameManager.instance.Lose();
            gameObject.SetActive(false);
        }


    }
}
