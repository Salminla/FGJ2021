using System;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private bool floatMovement = true;
    [SerializeField] private float floatMagnitude = 0.1f;
    private float yOrig;

    private void Start()
    {
        yOrig = transform.position.y;
    }

    void Update()
    {
        if (floatMovement)
            transform.position = new Vector3(transform.position.x, yOrig + floatMagnitude * Mathf.Sin(2 * Time.time), transform.position.z) ;
    }
}
