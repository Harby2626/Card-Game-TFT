using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Character : MonoBehaviour
{
    public abstract CharacterType characterType { get; }
    public Character_SO characterObject;
    public float Health { get; protected set; }
    public bool IsDead { get { return Health <= 0; } }
    public bool HaveTarget { get => currentTarget != null; }
    protected Character currentTarget;
    protected NavMeshAgent agent;
    protected float lastAttackTime;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Health = characterObject.health;
    }
    private void Update()
    {
        if (!HaveTarget) RequestTarget();
        else
        {
            if (CanAttack(characterObject.attackCooldown))
                Attack();
            else
                ChaseTarget();

        }
    }
    public void RequestTarget()
    {
        Character character = Character_Manager.Instance.RequestTarget(characterType);
        SetTarget(character);
    }
    public void SetTarget(Character character)
    {
        currentTarget = character;
    }

    public void ClearTarget()
    {
        currentTarget = null;
    }

    public bool CanAttack(float cooldown)
    {
        return currentTarget != null
            && Time.time >= lastAttackTime + cooldown
            && agent.remainingDistance <= characterObject.attackRange;
    }

    public void Attack()
    {
        agent.isStopped = true;
        lastAttackTime = Time.time;
        currentTarget.TakeDamage(characterObject.attackDamage);
    }

    private void ChaseTarget()
    {
        float destinationDistance = Vector3.Distance(agent.destination, currentTarget.transform.position);
        if (destinationDistance >= .2f)
            agent.SetDestination(currentTarget.transform.position);
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        Debug.Log($"{gameObject.name} {damage} Damaged");
        Health -= damage;
        if (Health <= 0) Die();
    }
    public void Die()
    {
        if (characterType == CharacterType.PLAYER)
        {
            Character_Manager.Instance.RemovePlayer(GetComponent<PlayerCharacter>());
        }
        else if (characterType == CharacterType.ENEMY)
        {
            Character_Manager.Instance.RemoveEnemy(GetComponent<EnemyCharacter>());
        }
        // todo Character death behavior
        Destroy(gameObject);
    }
}
