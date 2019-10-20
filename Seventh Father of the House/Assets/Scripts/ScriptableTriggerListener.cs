using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptableTriggerListener : MonoBehaviour
{
    public ScriptableTrigger trigger;

    public UnityEvent triggerEvent;

    public void OnTrigger()
    {
        triggerEvent?.Invoke();
    }

    private void OnEnable()
    {
        trigger.RegisterListener(OnTrigger);
    }

    private void OnDisable()
    {
        trigger.UnRegisterListener(OnTrigger);
    }
}
