using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : AttributesSync
{
    public GameObject BombPrefab;
    public Alteruna.Avatar avatar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!avatar.IsMe)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject bomb = Instantiate(BombPrefab, this.transform.position, Quaternion.identity);

                bomb.GetComponent<RigidbodySynchronizable>().AddForce(transform.forward * 20, ForceMode.Impulse);
            }
        return;
        }
    }
}
