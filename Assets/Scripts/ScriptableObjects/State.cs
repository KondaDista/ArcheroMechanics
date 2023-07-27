using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public bool isFinished { get; protected set; }
    [HideInInspector] public Enemy enemy;

    public virtual void Initialize(){}
    public abstract void Run();
}
