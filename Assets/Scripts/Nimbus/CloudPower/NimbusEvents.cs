using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbusEvents
{
    public static event Action OnGameStart;
    public static event Action OnNimbusStart;
    public static event Action OnCloudLatched;
    public static event Action OnJumped;
    public static event Action OnJumpFalling;
    public static event Action OnCloudPowerDepleted;
    public static event Action OnResting;
    public static event Action OnFalling;
    public static event Action OnGameEnd;
    public static event Action OnStageChange;

    public static void TriggerOnNimbusStart() => OnNimbusStart?.Invoke();
    public static void TriggerOnCloudLatched() => OnCloudLatched?.Invoke();
    public static void TriggerOnJumped() => OnJumped?.Invoke();
    public static void TriggerOnJumpFalling() => OnJumpFalling?.Invoke();
    public static void TriggerOnCloudPowerDepleted() => OnCloudPowerDepleted?.Invoke();
    public static void TriggerOnResting() => OnResting?.Invoke();
    public static void TriggerOnFalling() => OnFalling?.Invoke();
    public static void TriggerOnGameEnd() => OnGameEnd?.Invoke();
    public static void TriggerOnGameStart() => OnGameStart?.Invoke();
    public static void TriggerOnStageChange() => OnStageChange?.Invoke();
}
