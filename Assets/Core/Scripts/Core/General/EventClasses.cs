using UnityEngine.Events;
using Core.StatusEffect;
/// <summary>
/// Class to use Events with an Integer Parameter
/// </summary>
public sealed class EventInt : UnityEvent<int>
{

}

/// <summary>
/// Class to use Events with an Long Parameter
/// </summary>
public sealed class EventLong : UnityEvent<long>
{

}

/// <summary>
/// Class to use Events with an StatusEffect. The StatusEffect is an active component on an Object.
/// </summary>
public sealed class EventStatusEffect : UnityEvent<ActiveStatusEffect>
{

}