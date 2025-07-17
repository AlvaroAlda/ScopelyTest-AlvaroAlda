namespace Creeps.StatusEffects
{
    public abstract class BaseStatusEffect
    {
        public float Duration { get; protected set; }

        public BaseStatusEffect(float duration)
        {
            Duration = duration;
        }

        public abstract void Apply(Creep creep);
        public abstract void Remove(Creep creep);
        public virtual void Update(Creep creep, float deltaTime)
        {
            Duration -= deltaTime;
        }

        public bool IsExpired => Duration <= 0f;
    }
}
