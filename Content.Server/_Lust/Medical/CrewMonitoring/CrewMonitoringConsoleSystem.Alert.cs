using System.Linq;
using Content.Shared.Medical.SuitSensor;

namespace Content.Server.Medical.CrewMonitoring;

// Теперь мониторы Бригмеда и ОСЩ пиликают только на свои цели, а не на весь экипаж.
public sealed partial class CrewMonitoringConsoleSystem
{
    private bool HasVisibleCorpsesOutsideMorgue(
        EntityUid uid,
        CrewMonitoringConsoleComponent component)
    {
        var sensors = component.ConnectedSensors.Values.ToList();
        ApplyFilter(uid, ref sensors);

        foreach (var sensor in sensors)
        {
            var damagePercentage = sensor.DamagePercentage;
            var isCritical = damagePercentage.HasValue
                && damagePercentage.Value >= CriticalDamagePercentage;

            if (sensor.IsAlive && !isCritical)
                continue;

            if (!TryGetEntity(sensor.OwnerUid, out var ownerUid))
                continue;

            if (!IsEntityInMorgue(ownerUid.Value))
                return true;
        }

        return false;
    }
}