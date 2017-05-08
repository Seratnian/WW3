using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    public Save(float num, string s)
    {
        someNumber = num;
        someString = s;
    }

    public float someNumber { get; private set; }
    public string someString { get; private set; }
}