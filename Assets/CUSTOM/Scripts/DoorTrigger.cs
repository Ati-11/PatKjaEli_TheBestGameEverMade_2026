
using Unity.VisualScripting;
using UnityEngine;


public class DoorTrigger : MonoBehaviour
{

    public MUDoorToggle toggle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            return;
        }
        Debug.Log("Trigger Enter " + other.name);
        
        toggle.isInDoorVolume = true;

    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        Debug.Log("Trigger Exit" + other.name);
        toggle.isInDoorVolume = false;


    }

}
