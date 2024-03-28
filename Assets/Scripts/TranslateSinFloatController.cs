using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateSinFloatController : MonoBehaviour
{
    public Transform BirdMesh;
    public float SpeedForward = 1f;
    public float ForwardTravelDistance = 20f;
    public float SpeedUpDown = 1f;
    public float DistanceUpDown = 1f;

    private Vector3 initLocalPosition;

    private void Awake()
    {
        initLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (Vector3.Distance(initLocalPosition, transform.position) > ForwardTravelDistance)
            transform.localPosition = initLocalPosition;
        else
            transform.Translate(transform.forward * SpeedForward * Time.deltaTime, Space.World);

        BirdMesh.localPosition = new Vector3(BirdMesh.localPosition.x, Mathf.Sin(SpeedUpDown * Time.unscaledTime) * DistanceUpDown, BirdMesh.localPosition.z);       
    }
}
