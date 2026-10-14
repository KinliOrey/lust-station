using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Server._Lust.Medical;

public enum CrewMonitoringFakeState : byte
{
    Critical,
    Dead,
    Poor,
}

[RegisterComponent, AutoGenerateComponentPause]
public sealed partial class CrewMonitoringSpoofComponent : Component
{
    public EntityUid Rule;

    public CrewMonitoringFakeState FakeState;

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    [AutoPausedField]
    public TimeSpan EndTime;
}