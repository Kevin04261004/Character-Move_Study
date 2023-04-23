using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private Vector3 offset;
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.transform.position + offset, Time.deltaTime * 2);
    }

}
