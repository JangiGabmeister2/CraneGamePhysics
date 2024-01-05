using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fracture))]
public class BuildingFractureHandler : MonoBehaviour
{
    private Fracture _fracturer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WreckingBall"))
        _fracturer.CauseFracture();
    }
}
