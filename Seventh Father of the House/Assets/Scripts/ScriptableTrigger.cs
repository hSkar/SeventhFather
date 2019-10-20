using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Scriptable Trigger", menuName = "ScriptableTrigger")]
public class ScriptableTrigger : ScriptableObject
{
    private Action Trigger;


    public void DoTrigger()
    {
        Trigger?.Invoke();
    }

    public void RegisterListener(Action listener)
    {
        Trigger += listener;
    }

    public void UnRegisterListener(Action listener)
    {
        Trigger -= listener;
    }
}
