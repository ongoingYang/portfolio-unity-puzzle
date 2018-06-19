using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : Block {

    public bool isSelected;
    public delegate void EventHandler(SwitchBlock block);
    public event EventHandler BlockClickEvent;


    public override void Initialize(int origin, int j, int k){

        if (k > 0)
        {
            this.transform.position = new Vector3(IndexToNegative(j), 0, IndexToPositive(k));
            identifier = Identifier.Positive;
        }
        else {
            this.transform.position = new Vector3(k, IndexToNegative(j), k);
            identifier = Identifier.Negative;
        }
        switch (origin)
        {
            case 0:
                
                break;
            case 1:

                break;

            case 2:
                break;
        }


        SetParentOrigin(origin);
    }
    private void OnMouseDown()
    {
        BlockClickEvent(this);
    }
    public override void Interaction()
    {
        base.Interaction();
    }
}
