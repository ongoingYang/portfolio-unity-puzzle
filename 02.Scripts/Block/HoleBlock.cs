using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBlock : Block {

    public override void Initialize(int origin, int j, int k){
        base.Initialize(origin, j, k);

        if (k == LevelDesigner.BlockSize)
        {
            this.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.up);
            identifier = Identifier.Negative;
        }
        else if (j == LevelDesigner.BlockSize)
        {
            this.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.up);
            identifier = Identifier.Positive;

        }
        
    }
}