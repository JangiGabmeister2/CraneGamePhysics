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
            child.mass = rb_parent.mass;
            child.drag = rb_parent.drag;
            child.useGravity = rb_parent.useGravity;
            child.interpolation = rb_parent.interpolation;
            child.collisionDetectionMode = rb_parent.collisionDetectionMode;
        }

        foreach (HingeJoint child in hj_links)
        {
            child.useSpring = hj_parent.useSpring;
            
            //not confusing at all
            JointSpring spring = child.spring;
            spring.spring = hj_parent.spring.spring;
            child.spring = spring;
        }
    }
}
