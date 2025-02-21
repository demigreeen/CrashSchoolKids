using UnityEngine;

public class Door : MonoBehaviour
{

    public Transform rotateDoor;
    public BoxCollider colliderDoor;
    public Transform goalRotation;
    public float speed = 10f;
    public LayerMask ignoreMasks;

    private Quaternion startRotation;
    private bool isOpening = false;
    private GameObject currGo;

    private void Start()
    {
        startRotation = rotateDoor.rotation;
    }
    void Update()
    {
       if (isOpening == true)
       {
            rotateDoor.rotation = Quaternion.Lerp(rotateDoor.rotation, goalRotation.rotation, speed * Time.deltaTime);
       }
       else
       {
            rotateDoor.rotation = Quaternion.Lerp(rotateDoor.rotation, startRotation, speed * Time.deltaTime);
            currGo = null;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.layer != LayerMask.NameToLayer("Ground") && other.transform.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            isOpening = true;
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
