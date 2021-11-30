using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //patroli
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //serang
    public float timeBetweenAttacks;
    public bool alreadyAttack;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        //ambil posisi player
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange)
        {                               
            patroli();                              
        }

        if(playerInSightRange && !playerInAttackRange)
        {
            kejar();
        }

        if(playerInSightRange && playerInAttackRange)
        {
            serang();
        }
    }

    private void patroli()
    {
        if(!walkPointSet)
        {
            searchwalkPoint();
        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //musuh sampai tujuan
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        } 
    }
    
    private void searchwalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    

    private void kejar()
    {
        agent.SetDestination(player.position);
    }

    private void serang()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttack)
        {
            //script attack
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttack = false;
    }
}
