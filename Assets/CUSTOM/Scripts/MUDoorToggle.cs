using UnityEngine;


[RequireComponent(typeof(Alteruna.AnimationSynchronizable))]
public class MUDoorToggle : MonoBehaviour
{

    private Alteruna.AnimationSynchronizable _aniSync;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _aniSync = GetComponent<Alteruna.AnimationSynchronizable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // play for all clients
            _aniSync.SetBool("Open Right",true);
            _aniSync.SetBool("Open Left", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // play for all clients
            _aniSync.SetBool("Open Right", false);
            _aniSync.SetBool("Open Left", false);
        }
    }
}
