﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float intialValue;
    [HideInInspector]
    public float runtimeValue;

    public void OnAfterDeserialize()
    {
        runtimeValue = intialValue;
    }
    public void OnBeforeSerialize()
    {

    }
}
