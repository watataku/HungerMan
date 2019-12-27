using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject item;

    private NavMeshAgent agent;

    private float putInterval;
    private int existItems;

    // Start is called before the first frame update
    void Start()
    {
        putInterval = 3f;
        existItems = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(Random.Range(-13f, 13f), 0, Random.Range(-13f, 13f)));
    }

    // Update is called once per frame
    void Update()
    {
        // var playerDistance = Vector3.Distance(playerPosition.position, transform.position);

        if (agent.remainingDistance < 3.0f)
        {
            agent.SetDestination(new Vector3(Random.Range(-13f, 13f), 0, Random.Range(-13f, 13f)));
        }

        putInterval -= Time.deltaTime;
        if (putInterval < 0f && existItems < 4)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            putInterval = Random.Range(3f, 5f);
            existItems++;
        }
    }

    public void decrementItems()
    {
        existItems--;
    }
}
