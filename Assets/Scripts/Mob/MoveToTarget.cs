using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    private NavMeshAgent _agent = default;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        TargetPlayer();
        GetComponent<Animator>().SetBool("Walk Forward", true);
    }

    private void TargetPlayer()
    {
    }

    private void Update()
    {
        if (_agent.isActiveAndEnabled)
        {
            _agent.SetDestination(FindObjectOfType<Player>().transform.position);
        }
    }
}
