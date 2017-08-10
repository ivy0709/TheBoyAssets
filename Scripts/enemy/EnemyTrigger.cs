using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

    public List<GameObject> enemylist;
    public List<Transform> poslist;

    public float WaitTime = 0.0f;
    public float IntervalTime = 3.0f;

    private bool hasCreated = false;


    private void OnTriggerEnter(Collider other)
    {
        if(hasCreated)
        {
            return;
        }
        hasCreated = true;
        if (other.tag == "Player")
        {
            StartCoroutine(CreatEnemy());
        }
    }

    IEnumerator CreatEnemy()
    {
        yield return new WaitForSeconds(WaitTime);
        foreach(GameObject go in enemylist)
        {
            foreach(Transform pos in poslist)
            {
                GameObject.Instantiate(go, pos.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(IntervalTime);
        }
    }
}
