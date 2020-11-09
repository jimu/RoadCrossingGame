using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649,414
[RequireComponent(typeof(SpriteRenderer))]
public class FrameAnimation : MonoBehaviour
{
    [SerializeField] int frameRate = 6; // inspector
    [SerializeField] Sprite[] frames;

    SpriteRenderer spriteRenderer;
    int currentFrame = -1;
    int numFrames;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        numFrames = frames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        int desiredFrame = Mathf.FloorToInt(Time.time * frameRate) % numFrames;
        float ftime = Time.time * frameRate;
        spriteRenderer.sprite = frames[desiredFrame];
    }
}
