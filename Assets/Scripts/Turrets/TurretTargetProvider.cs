using System.Collections.Generic;

public static class TurretTargetProvider
{
    public static HashSet<ITurretTarget> ActiveTurretTargets = new HashSet<ITurretTarget>();

    public static void AddActiveTarget(ITurretTarget target)
    {
        ActiveTurretTargets.Add(target);
    }

    public static void RemoveActiveTarget(ITurretTarget target)
    {
        ActiveTurretTargets.Remove(target);
    }
}
