using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float damageRate = 0.5f;

    [SerializeField]
    private List<GameObject> playerList;

    public int attackDamage;

    private void Start()
    {
        // 0.5s 调用一次 
        InvokeRepeating("Attack", 0, damageRate);
    }
    // Update is called once per frame
    void Update ()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
	}

    private void Attack()
    {
        foreach(GameObject go in playerList)
        {
            go.SendMessage("TakeDamage", attackDamage * damageRate, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(playerList.IndexOf(other.gameObject) < 0)
            {
                playerList.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            if (playerList.IndexOf(other.gameObject) >= 0)
            {
                playerList.Remove(other.gameObject);
            }
        }
    }
}
