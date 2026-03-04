using FMODUnity;
using UnityEngine;


[RequireComponent(typeof(Alteruna.AnimationSynchronizable))]
public class MUDoorToggle : MonoBehaviour
{

    private Alteruna.AnimationSynchronizable _aniSync;

    [Header("Audio")]
    [SerializeField] private EventReference doorSound;

    public bool isInDoorVolume = false;
    public bool doorIsOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _aniSync = GetComponent<Alteruna.AnimationSynchronizable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInDoorVolume)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E PRESSED");

                doorIsOpen = !doorIsOpen;

                RuntimeManager.PlayOneShot(doorSound, transform.position);
                // play for all clients
                _aniSync.SetBool("Open Right", doorIsOpen);
                _aniSync.SetBool("Open Left", doorIsOpen);
                
            }

        }
        
    }
}
