using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform target;


    void LateUpdate()
    {
        transform.position = new Vector3(0, target.position.y + 3, -10); // default camera is z=-10;
    }
}
