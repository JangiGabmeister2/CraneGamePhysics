using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BecomeRagdoll : MonoBehaviour
{
    [SerializeField] Rigidbody[] _rbs;

    private void Awake()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rbs in _rbs)
        {
            rbs.useGravity = false;
            rbs.isKinematic = true;
        }
    }

    public void Ragdollize()
    {
        foreach (Rigidbody rbs in _rbs)
        {
            rbs.useGravity = true;
            rbs.isKinematic = false;
        }
    }
}
