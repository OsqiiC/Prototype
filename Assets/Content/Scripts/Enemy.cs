using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Character
{
    public Transform ItemContainer;
    public ObjectSpawner objectSpawner;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float playerSearchRadius = 5f;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackRadius;
    [SerializeField] private MeleeAttack attack;

    private Transform target;
    private bool isAttacking = false;
    private bool playerFound = false;

    private void Awake()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        entity.items.Add(ItemPrefabStorage.Instance.GetRandomItemData());
    }

    void Update()
    {
        Move();
        AttackAttempt();
        SearchForPlayer();
    }

    public void Initialize(Entity entity)
    {
        this.entity = entity;
        transform.position = entity.position.Vector3;
        transform.rotation = Quaternion.Euler(entity.rotation.Vector3);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public override void TakeDamage(float value)
    {
        base.TakeDamage(value);

        playerFound = true;
    }

    public void SetSpawnerData()
    {
        entity.rotation = new Entity.Vector3Json(transform.rotation.eulerAngles);
        entity.position = new Entity.Vector3Json(transform.position);
        objectSpawner.SaveEnemyData(entity);
    }

    protected override void Death()
    {
        base.Death();

        foreach (var item in entity.items)
        {
            var itemInstance = Instantiate(ItemPrefabStorage.Instance.GetItemPrefab(item.itemID), ItemContainer);
            itemInstance.transform.position = transform.position;
        }

        objectSpawner.RemoveEnemyData(entity,this);
    }

    private void SearchForPlayer()
    {
        if (playerFound) return;
        if (target == null) return;
        if (Vector3.Distance(transform.position, target.position) > playerSearchRadius) return;

        playerFound = true;
    }

    private void Move()
    {
        if (target == null || !playerFound) return;
        navMeshAgent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
    }

    private void AttackAttempt()
    {
        if (target == null || isAttacking) return;
        if (Vector3.Distance(transform.position, target.position) > attackRadius) return;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackDelay);

        var attack = this.attack.Create(this, transform);
        isAttacking = false;
    }
}
