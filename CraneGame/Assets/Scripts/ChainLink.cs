using System.Collections.Generic;
using UnityEngine;

public class ChainLink : MonoBehaviour
{
    [SerializeField] List<Rigidbody> rb_links;
    [SerializeField] List<HingeJoint> hj_links;

    Rigidbody rb_parent;
    HingeJoint hj_parent;

    private void Awake()
    {
        rb_parent = GetComponent<Rigidbody>();
        hj_parent = GetComponent<HingeJoint>(); 

        Rigidbody[] rbs = rb_parent.GetComponentsInChildren<Rigidbody>();
        HingeJoint[] hjs = hj_parent.GetComponentsInChildren<HingeJoint>();

        foreach (Rigidbody item in rbs) rb_links.Add(item);
        rb_links.Remove(rb_parent);

        foreach (HingeJoint item in hjs) hj_links.Add(item);
        hj_links.Remove(hj_parent);
    }

    private void OnValidate()
    {
        foreach (Rigidbody child in rb_links)
        {
            Rigidbody parent = rb_parent;

            child.mass = parent.mass;
            child.drag = parent.drag;
            child.useGravity = parent.useGravity;
            child.interpolation = parent.interpolation;
            child.collisionDetectionMode = parent.collisionDetectionMode;
        }

        foreach (HingeJoint child in hj_links)
        {
            HingeJoint parent = hj_parent;

            child.useSpring = parent.useSpring;
            
            //not confusing at all
            JointSpring spring = child.spring;
            spring.spring = parent.spring.spring;
            child.spring = spring;
        }
    }
}
