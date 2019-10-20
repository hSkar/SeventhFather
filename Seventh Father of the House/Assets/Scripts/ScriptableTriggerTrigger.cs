using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableTriggerTrigger : MonoBehaviour
{
    public ScriptableTrigger trigger;

    public void Trigger()
    {
        if(trigger != null)
            trigger.DoTrigger();
    }
}
