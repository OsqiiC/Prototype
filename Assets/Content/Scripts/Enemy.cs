using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Move()
    {
        if (target == null) return;
        navMeshAgent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
    }
}
