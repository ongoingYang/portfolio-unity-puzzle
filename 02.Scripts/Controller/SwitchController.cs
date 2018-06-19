using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    
    public enum SwitchState
    {
        Both,
        None,
        Negative,
        Positive
    }

    public void LinkSwitchToController(SwitchBlock negative, SwitchBlock positive)
    {
        negativeSwitch = negative;
        positiveSwitch = positive;
        negativeSwitch.BlockClickEvent += DetectBlockClick;
        positiveSwitch.BlockClickEvent += DetectBlockClick;
    }
    private void OnDisable()
    {
        negativeSwitch.BlockClickEvent -= DetectBlockClick;
        positiveSwitch.BlockClickEvent -= DetectBlockClick;
    }
    private void Awake()
    {
        maxIndex = LevelDesigner.BlockSize - 1; //holeGroup 배열 인덱스 최대
        minIndex = 0; // holeGroup 배열 인덱스 최소
    }
    public SwitchBlock negativeSwitch;
    public SwitchBlock positiveSwitch;

    public HoleBlock[] holeGroup; //디자이너에서 인스턴스 순서대로
    public int holeGroupIndicator; // minIndex ~ MaxIndex
    public int minIndex;
    public int maxIndex;
    public bool isActivated;

    public SwitchController[] neighborController = new SwitchController[2];

    public void DetectBlockClick(SwitchBlock block)
    {
        if (neighborController[0].isActivated || neighborController[1].isActivated) return;
        HandleWithSwitch(block, GetPrevSwitchState());

    }

    public SwitchState GetPrevSwitchState()
    {
        SwitchState note = SwitchState.None;
        if       (negativeSwitch.isSelected && positiveSwitch.isSelected) note = SwitchState.Both;
        else if (!negativeSwitch.isSelected && positiveSwitch.isSelected) note = SwitchState.Negative;
        else if (!negativeSwitch.isSelected && positiveSwitch.isSelected) note = SwitchState.Positive;
        return note;
    }
    public void DeActiveSwitchBlock()
    {
        negativeSwitch.isSelected = false;
        positiveSwitch.isSelected = false;
        isActivated = false;
        holeGroupIndicator = -1; // 아무것도 가리키지 않는다는 의미
    }

    public void HandleWithSwitch(SwitchBlock click, SwitchState switchState)
    {
        switch (switchState)
        {
            case SwitchState.None:

                if (click.identifier == Block.Identifier.Negative){
                    click.isSelected = true;
                    holeGroupIndicator = minIndex;
                }
                else if (click.identifier == Block.Identifier.Positive){
                    click.isSelected = true;
                    holeGroupIndicator = maxIndex;
                }
                break;

            case SwitchState.Both:

                if (click.identifier == Block.Identifier.Negative)
                {
                    holeGroupIndicator--;
                    if (holeGroupIndicator < minIndex) DeActiveSwitchBlock();
                    else isActivated = true;
                }
                else if (click.identifier == Block.Identifier.Positive)
                {
                    holeGroupIndicator++;
                    if (holeGroupIndicator > maxIndex) DeActiveSwitchBlock();
                    else isActivated = true;
                }

                break;
            case SwitchState.Negative:
                if (click.identifier == Block.Identifier.Negative)
                {
                    DeActiveSwitchBlock();
                }
                else if (click.identifier == Block.Identifier.Positive)
                {
                    holeGroupIndicator++;
                    isActivated = true;
                }
                
                break;
            case SwitchState.Positive:
                if (click.identifier == Block.Identifier.Negative)
                {
                    holeGroupIndicator--;
                    isActivated = true;
                }
                else if (click.identifier == Block.Identifier.Positive)
                {
                    DeActiveSwitchBlock();
                }
                break;
        }
    }

}
