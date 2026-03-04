using UnityEditor;
using UnityEngine;
using Alteruna;

public class enemyBasic : MonoBehaviour
{
    public float enemySpeed = 5f;
    public float enemyDetectRange = 10f;
    private Transform player;
    private Rigidbody Rigid;
    //private Alteruna.Avatar Avatar;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Avatar = GetComponent<Alteruna.Avatar>();
        Rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > enemyDetectRange)
        {
            Rigid.angularVelocity = Vector3.zero;
        }
        else
        {
            Vector3 direction = (player.position - transform.position).normalized; 
            Vector3 lookDirection = new Vector3(direction.x, 0f, direction.z); 
            transform.rotation = Quaternion.LookRotation(lookDirection); 
            Vector3 velocity = direction * enemySpeed; 
            Rigid.linearVelocity = new Vector3(velocity.x, Rigid.linearVelocity.y, velocity.z);
        }
    }
}
