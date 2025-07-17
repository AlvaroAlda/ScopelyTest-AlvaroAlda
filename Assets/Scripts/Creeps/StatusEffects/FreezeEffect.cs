namespace Creeps.StatusEffects
{
    public class FreezeEffect : BaseStatusEffect
    {
        private readonly float _slowdown;
    
        public FreezeEffect(float duration, float slowdown) : base(duration)
        {
            _slowdown = slowdown;
        }

        public override void Apply(Creep creep)
        {
            creep.SpeedMultiplier *= _slowdown;
        }

        public override void Remove(Creep creep)
        {
            creep.SpeedMultiplier /= _slowdown;
        }
    }
}
