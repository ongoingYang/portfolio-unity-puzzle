using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : Block {


    public override void Initialize(int origin, int j, int k)
    {

        this.transform.position = new Vector3(0, IndexToNegative(j), 0);
        SetParentOrigin(origin);
        this.transform.rotation = Quaternion.Euler(0, -90f, 90f);
    }
}
