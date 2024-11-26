using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using BTAI;
using TMPro;
using UnityEngine.AI;
using Random = System.Random;

//skeleton (essentially a patroller); wandering with this attached too
public class BehaviorMinion : MonoBehaviour
{
    public TMP_Text attackUI;
    public WanderBehavior wanderBehaviorScript; //reference to wander script
    public Transform player;
    public Transform NPC;
    public Transform boundary; //cube for home area
    public Transform retreatSpot; //sphere for npc area  
    public float detectionRange = 2f;
    public float speed = 3f; 
    bool InBoundary = false;
    bool IsAttacking = false;

    Vector3 target;
    private Root m_btRoot = BT.Root(); //root node

    // Start is called before the first frame update
    void Start()
    {
        attackUI.enabled = false; //hide UI

        //Setup:
        player = GameObject.FindGameObjectWithTag("Player").transform;
        NPC = transform;
        boundary = GameObject.FindGameObjectWithTag("Boundary").transform;
        retreatSpot = GameObject.FindGameObjectWithTag("Origin").transform;
        wanderBehaviorScript = GetComponent<WanderBehavior>(); //get wander component 

        Selector selector = BT.Selector(); //start the branch, choose sequence 
        selector.OpenBranch(); 
        m_btRoot.OpenBranch(selector); //connect the branch

        Sequence attackSequence = BT.Sequence(); 
        Sequence chaseSequence = BT.Sequence(); 
        Sequence retreatSequence = BT.Sequence(); 
        Sequence wanderSequence = BT.Sequence();

        // Setup nodes
        attackSequence.OpenBranch(
            BT.Condition(() => InRange()), // If in range
            BT.RunCoroutine(() => Attack("You've been hit!")) // Attack action
        );

        chaseSequence.OpenBranch(
            BT.Condition(() => InRange()), // If in range
            BT.RunCoroutine(() => Chase()) // Chase action
        );

        retreatSequence.OpenBranch(
            BT.Condition(() => InBoundary), // If in boundary
            BT.RunCoroutine(() => Retreat()) // Retreat action
        );

        wanderSequence.OpenBranch(
            BT.Condition(() => !InRange() && !InBoundary),
            BT.RunCoroutine(() => Wander()) // Use the new Wander coroutine
        );

        //add sequence to selector
        selector.OpenBranch(attackSequence, chaseSequence, retreatSequence, wanderSequence);

}

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick(); //continue through tree 
    }

    bool InRange()
    {
        float distance = Vector3.Distance(player.position, NPC.position);

        if (distance <= detectionRange)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary")) //if in home area
        {
            InBoundary = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            InBoundary = false;
        }
    }

    //action: attack node
    private IEnumerator<BTState> Attack(String text)
    {
        if (!InRange())
        {
            attackUI.enabled = false;
            yield return BTState.Failure;
        }

        IsAttacking = true; 
        attackUI.enabled = true;
        attackUI.text = text;

        yield return BTState.Success;
    }

    //action: chase (follow players last position)
    private IEnumerator<BTState> Chase()
    {
        if (!InRange()) //if not in range 
        {
            yield return BTState.Failure;
        }

        Vector3 direction = (player.position - NPC.position).normalized;

        //rotate NPC
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            NPC.rotation = Quaternion.Slerp(NPC.rotation, lookRotation, Time.deltaTime * speed); //rotate smoothly
        }

        NPC.position += direction * speed * Time.deltaTime; //move NPC

        yield return BTState.Continue;
    }

    //action: retreat (to a specific transform)
    private IEnumerator<BTState> Retreat()
    {
        if (!InBoundary) //if not in boundary
        {
            yield return BTState.Failure;
        }

        Vector3 direction = (retreatSpot.position - NPC.position).normalized;

        //rotate NPC
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            NPC.rotation = Quaternion.Slerp(NPC.rotation, lookRotation, Time.deltaTime * speed); //rotate smoothly
        }

        NPC.position += direction * speed * Time.deltaTime; // Move NPC backward

        yield return BTState.Continue;
    }

    //wander code from Wander script 
    private IEnumerator<BTState> Wander()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        Vector3 randomPoint = NPC.position + (UnityEngine.Random.insideUnitSphere * 10f);
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            target = hit.position;
        }

        agent.SetDestination(target);

        // wait for agent to reach destination
        while (agent.remainingDistance > 0.1f)
        {
            yield return BTState.Continue;
        }

        yield return BTState.Success;
    }
}
