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
        if (trigger == null)
        {
            this.enabled = false;
            return;
        }
            

        trigger.RegisterListener(OnTrigger);
    }

    private void OnDisable()
    {
        if (trigger == null)
            return;

        trigger.UnRegisterListener(OnTrigger);
    }
}
