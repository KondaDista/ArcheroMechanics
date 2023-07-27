using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melle : Enemy
{
    private float moveStartDistance;

    public override void Start()
    {
        base.Start();
        moveStartDistance = _moveDistance;
    }

    public override void Update()
    {
        base.Update();
        if (!isVisbleTarget)
            _moveDistance = 2f;
        
        else if (isVisbleTarget)
            _moveDistance = moveStartDistance;
    }
}
