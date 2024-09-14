using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbusEvents
{
    public static event Action OnCloudLatched;
    public static event Action OnJumped;
    public static event Action OnCloudPowerDepleted;
    public static event Action OnNimbusResting;
    public static void TriggerOnCloudLatched() => OnCloudLatched?.Invoke();
    public static void TriggerOnJumped() => OnJumped?.Invoke();
    public static void TriggerOnCloudPowerDepleted() => OnCloudPowerDepleted?.Invoke();
    public static void TriggerOnNimbusResting() => OnNimbusResting?.Invoke();
}
