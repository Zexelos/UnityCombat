public interface ICombat
{
    void DealDamage(float damage);

    void TakeDamage(float damage);

    bool IsAlive { get; }
}
