using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer.Samples;
using UnityEngine;

public class SliceItem : MonoBehaviour
{
    [Header("Slice")]
    public MeshFilter[] slicePlanes;
    [Space(10)]
    [Header("Just Remove Object")]
    public bool isJustRemove;
    public List<GameObject> removeObj;
    [Space(10)]
    public AudioSource audio;

    bool complete;


    bool[] isSliced;
    IBzSliceableNoRepeat sliceObj;
    Rigidbody rigidbody;
    private void Start()
    {
        isSliced = new bool[slicePlanes.Length];
        rigidbody = GetComponent<Rigidbody>();
        sliceObj = GetComponent<IBzSliceableNoRepeat>();
    }

    private void Update()
    {
        if (rigidbody.isKinematic == true)
        {
            complete = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (rigidbody.isKinematic == false && collision.gameObject.layer == LayerMask.NameToLayer("Ground") && rigidbody.velocity.y < 0.2f && rigidbody.velocity.y > -0.2f)
        {
            if (complete == false)
            {
                if (isJustRemove == false && isSliced.Length != 0)
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
                    complete = true;
                }
                else
                {
                    foreach (var obj in removeObj)
                    {
                        if (obj.transform.parent != null)
                        {
                            obj.transform.parent = null;
                            obj.AddComponent<Rigidbody>();
                        }
                    }
                    complete = true;

                }

                if (transform.name != "Kaktus_neg")
                {
                    audio.Play();
                }
                
            }
        }
    }
}
