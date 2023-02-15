using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract CharacterType characterType { get; }
    public Character_SO characterObject;
    public float Health { get; protected set; }
    public bool IsDead { get { return Health <= 0; } }
    public bool HaveTarget { get => currentTarget != null; }
    protected Character currentTarget;
    protected float lastAttackTime;
    private void Awake()
    {
        Health = characterObject.health;
    }
    private void Update()
    {
        if (!HaveTarget) RequestTarget();
        else Attack();
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
        return currentTarget != null && Time.time >= lastAttackTime + cooldown;
    }

    public void Attack()
    {
        if (CanAttack(characterObject.attackCooldown))
        {
            lastAttackTime = Time.time;
            currentTarget.TakeDamage(characterObject.attackDamage);
        }
        else
        {
            ChaseTarget();
        }
    }

    private void ChaseTarget()
    {
        // todo chase target
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        Health -= damage;
        if (Health <= 0) Die();
    }
    public void Die()
    {
        if (characterType == CharacterType.PLAYER)
        {
            Character_Manager.Instance.playerCharacters.Remove(GetComponent<PlayerCharacter>());
            Character_Manager.Instance.CheckWaveEnded();
        }
        else if (characterType == CharacterType.ENEMY)
        {
            Character_Manager.Instance.aliveEnemyCharacters.Remove(GetComponent<EnemyCharacter>());
            Character_Manager.Instance.DidPlayerFailed();
        }
        Destroy(gameObject);
        // todo Character death behavior
    }
}
