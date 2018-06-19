using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Block : MonoBehaviour {


    public Func<int, float> IndexToPositive = x => 1 + x;
    public Func<int, float> IndexToNegative = x => -1 * (1 + x);


    public enum ParentDirection {
        xDirection,
        yDirection,
        zDirection
        };
    public ParentDirection parentDirection;

    public enum Identifier
    {
        Positive,
        Negative
    }
    public Identifier identifier;

    public virtual void Initialize(int origin, int j , int k){

        this.transform.position = new Vector3(IndexToNegative(j), 0, IndexToPositive(k));
        SetParentOrigin(origin);
    }
    protected void SetParentOrigin(int origin)
    {
        switch (origin)
        {
            case 0:
                parentDirection = ParentDirection.xDirection;
                this.transform.SetParent(LevelDesigner.parentTransformX);
                break;
            case 1:
                parentDirection = ParentDirection.yDirection;
                this.transform.SetParent(LevelDesigner.parentTransformY);
                break;
            case 2:
                parentDirection = ParentDirection.zDirection;
                this.transform.SetParent(LevelDesigner.parentTransformZ);
                break;
        }
    }

    public virtual void Interaction()
    {


    }

}
