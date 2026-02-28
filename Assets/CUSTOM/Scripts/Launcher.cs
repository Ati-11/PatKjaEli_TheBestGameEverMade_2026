using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject BombPrefab;
    public Alteruna.Avatar avatar;

    private void Awake()
    {
        avatar = GetComponent<Alteruna.Avatar>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!avatar.IsMe)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject bomb = Instantiate(BombPrefab, this.transform.position, Quaternion.identity);

            bomb.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
        }
    }
}
