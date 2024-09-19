using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;

    float startingZ;

    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float distanceFromTarget => transform.position.z - followTarget.position.z;

    float clippingPlane => (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float paralaxFactor => Mathf.Abs(distanceFromTarget) / clippingPlane;
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + canMoveSinceStart * paralaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
