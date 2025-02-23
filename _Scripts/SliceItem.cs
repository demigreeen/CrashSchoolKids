using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer.Samples;
using UnityEngine;

public class SliceItem : MonoBehaviour
{
    public MeshFilter[] slicePlanes;

    bool[] isSliced;
    IBzSliceableNoRepeat sliceObj;
    Rigidbody rigidbody;
    private void Start()
    {
        isSliced = new bool[slicePlanes.Length];
        rigidbody = GetComponent<Rigidbody>();
        sliceObj = GetComponent<IBzSliceableNoRepeat>();
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (rigidbody.isKinematic == false && collision.gameObject.layer == LayerMask.NameToLayer("Ground") && rigidbody.velocity.y < 0.1f && rigidbody.velocity.y > -0.1f)
        {
            for (int i = 0; i < slicePlanes.Length; i++)
            {
                if (isSliced[i] == false)
                {
                    MeshFilter filter = slicePlanes[i];
                    Vector3 normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
                    var plane = new Plane(normal, filter.gameObject.transform.position);
                    sliceObj.Slice(plane, 0, null);
                    isSliced[i] = true;
                }
            }
        }
    }
}
