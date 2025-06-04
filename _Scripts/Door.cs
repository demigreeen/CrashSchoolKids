using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Transform rotateDoor;
    public BoxCollider colliderDoor;
    public Transform goalRotation;
    public float speed = 10f;
    public LayerMask ignoreMasks;
    public AudioSource audioOpen;
    public AudioSource audioClose;

    private Quaternion startRotation;
    private bool isOpening = false;
    private GameObject currGo;

    

    private void Start()
    {
        startRotation = rotateDoor.rotation;

        if (audioOpen.volume == 0 || audioClose.volume == 0) { audioOpen.volume = 0; audioClose.volume = 0; }
    }
    void Update()
    {
       if (isOpening == true)
       {
            rotateDoor.rotation = Quaternion.Lerp(rotateDoor.rotation, goalRotation.rotation, speed * Time.deltaTime);
       }
       else if(rotateDoor.rotation != startRotation)
       {
            rotateDoor.rotation = Quaternion.Lerp(rotateDoor.rotation, startRotation, speed * Time.deltaTime);
            if (audioClose.isPlaying == false)
            {
                audioClose.Play();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (isOpening == false && other.transform.gameObject.layer != LayerMask.NameToLayer("Ground") && other.transform.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            if(audioOpen.isPlaying == false)
            {
                if (TutorialManager.instance.isTutorialOn == true && other.transform.CompareTag("Player"))
                {
                    
                }
                else
                {
                    audioOpen.Play();
                }
               
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.layer != LayerMask.NameToLayer("Ground") && other.transform.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            if (TutorialManager.instance.isTutorialOn == true && other.transform.CompareTag("Player"))
            {
                isOpening = false;
            }
            else
            {
                isOpening = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer != LayerMask.NameToLayer("Ground") && other.transform.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            isOpening = false;
        }
    }
}
