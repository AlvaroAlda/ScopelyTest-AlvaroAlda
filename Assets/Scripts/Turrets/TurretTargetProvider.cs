using System.Collections.Generic;

namespace Turrets
{
    public static class TurretTargetProvider
    {
        public static readonly HashSet<ITurretTarget> ActiveTurretTargets = new();

        public static void AddActiveTarget(ITurretTarget target)
        {
            ActiveTurretTargets.Add(target);
        }

        public static void RemoveActiveTarget(ITurretTarget target)
        {
            ActiveTurretTargets.Remove(target);
        }
    }
}
