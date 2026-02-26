
using UnityEngine;

public class MURobotTrigger : MonoBehaviour
{

    public EnemyAi enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        Debug.Log("AI triggered");

        enemy.player = other.transform;

    }

}
