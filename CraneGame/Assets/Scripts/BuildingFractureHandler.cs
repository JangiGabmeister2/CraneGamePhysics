using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fracture))]
public class BuildingFractureHandler : MonoBehaviour
{
    private GameObject _homeWrecker;
    private Fracture _fracturer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WreckingBall"))
            _homeWrecker = other.gameObject;

        _fracturer.CauseFracture();
    }
}
