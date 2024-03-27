using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{

    public Transform camTransform;
    public float additionalRotation;

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToCamera = camTransform.position - transform.position;
        directionToCamera.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        targetRotation *= Quaternion.Euler(0, -additionalRotation, 0);

        transform.rotation = targetRotation;
    }
}
