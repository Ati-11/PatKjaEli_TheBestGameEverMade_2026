using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Launcher : MonoBehaviour
{
    public GameObject BombPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject bomb = Instantiate(BombPrefab, this.transform.position, Quaternion.identity);

            bomb.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
        }
    }
}
