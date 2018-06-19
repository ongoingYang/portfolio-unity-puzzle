using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelDesigner : MonoBehaviour {
    public static LevelDesigner Instance;
    private void MakeInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    [SerializeField]
    private GameObject planeBlock;
    [SerializeField]
    private GameObject rotatorPrefab;
    [SerializeField]
    private GameObject swithBlockPrefab;
    [SerializeField]
    private GameObject holeBlockPrefab;
    [SerializeField]
    private SwitchController switchControllerPrefab;

    [SerializeField]
    private GameObject skinMeshPrefab;

  
    public PlaneBlock[,,] blockArray;
    public Rotator[,] rotatorArray;
    public SwitchBlock[,] switchArray;
    public SwitchController[,] switchController;
    
    public static Transform parentTransformX;
    public static Transform parentTransformY;
    public static Transform parentTransformZ;
    public static Transform skinMeshTransform;

    const int blockSize = 4; // default
    const int axisIndex = 3; // default
    public static int BlockSize{
        get { return blockSize; }
    }

    private void Awake()
    {
        blockArray = new PlaneBlock[3, BlockSize, BlockSize];
        rotatorArray = new Rotator[3, BlockSize];
        switchController = new SwitchController[3, 2]; // [axis(3), n,p(2)]
        switchArray = new SwitchBlock[3, 2]; // [axis(3), n,p(2)]

        


        parentTransformX = transform.GetChild(0);
        parentTransformY = transform.GetChild(1);
        parentTransformZ = transform.GetChild(2);
        skinMeshTransform = transform.GetChild(3);
    }
    private void Start(){

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++){

                SwitchController controller = Instantiate(switchControllerPrefab) as SwitchController;
                switchController[i, j] = controller;
            }
        }

        SetLevel();
        SetSwitchController();
    }
    private void SetSwitchController()
    {
        switchController[0, 0].LinkSwitchToController(switchArray[1, 0], switchArray[0, 1]); //negativeXcontroller
        switchController[0, 1].LinkSwitchToController(switchArray[2, 0], switchArray[0, 1]); //positiveXcontroller
        switchController[1, 0].LinkSwitchToController(switchArray[2, 0], switchArray[1, 1]); //negativeYcontroller
        switchController[1, 1].LinkSwitchToController(switchArray[0, 0], switchArray[1, 1]); //positiveYcontroller
        switchController[2, 0].LinkSwitchToController(switchArray[0, 0], switchArray[2, 1]); //negativeZcontroller
        switchController[2, 1].LinkSwitchToController(switchArray[1, 0], switchArray[2, 1]); //positiveZcontroller


    }
    private void SetPlaneBlock()
    {
        for (int i = 0; i < axisIndex; i++){ //axisIndex: 0 == x, 1 == y, 2 == z
            for (int j = 0; j < BlockSize; j++){
                for (int k = 0; k < BlockSize; k++){
                    GameObject block = Instantiate(planeBlock) as GameObject;
                    PlaneBlock planeblock = block.GetComponent<PlaneBlock>();
                    planeblock.Initialize(i, j, k);
                    blockArray[i, j, k] = planeblock;

                }
            }
        }
    }
    private void SetRotator()
    {
        for (int i = 0; i < axisIndex; i++) 
        {
            for (int j = 0; j < BlockSize; j++)
            {
                GameObject block = Instantiate(rotatorPrefab) as GameObject;
                Rotator rotator = block.GetComponent<Rotator>();
                rotator.Initialize(i, j, 0);
                rotatorArray[i, j] = rotator;
            }
                
        }
    }
    private void SetSwitchBlock()
    {
        for (int i = 0; i < axisIndex; i++) //axisIndex(0 -> x) axisIndex(1 -> y)axisIndex(2 -> z)
        {

            GameObject blockUnder = Instantiate(swithBlockPrefab) as GameObject;
            SwitchBlock switchBlockNegative = blockUnder.GetComponent<SwitchBlock>();
            switchBlockNegative.Initialize(i, blockSize, 0);
            switchArray[i, 0] = switchBlockNegative;

            GameObject blockUpper = Instantiate(swithBlockPrefab) as GameObject;
            SwitchBlock switchBlockPositive = blockUpper.GetComponent<SwitchBlock>();
            switchBlockPositive.Initialize(i, blockSize, blockSize);
            switchArray[i, 1] = switchBlockPositive;
        }
    }
    private void SetHoleBlock()
    {
        for (int i = 0; i < axisIndex; i++) 
        {
            int j = 0; int k = 0;
            while (j < blockSize) // negative
            {
                GameObject block = Instantiate(holeBlockPrefab) as GameObject;
                HoleBlock holeBlock = block.GetComponent<HoleBlock>();
                holeBlock.Initialize(i, j, blockSize);
                switchController[i, 0].holeGroup[j] = holeBlock;
                j++;
            }
            while (k < blockSize) // positive
            {
                GameObject block = Instantiate(holeBlockPrefab) as GameObject;
                HoleBlock holeBlock = block.GetComponent<HoleBlock>();
                holeBlock.Initialize(i, blockSize, k);
                switchController[i, 1].holeGroup[k] = holeBlock;

                k++;
            }
        }
    }
    private void SetSkinMesh()
    {
        for (int i = 0; i < BlockSize; i++)
        {
            for (int j = 0; j < BlockSize; j++)
            {
                for (int k = 0; k < BlockSize; k++)
                {
                    GameObject block = Instantiate(skinMeshPrefab) as GameObject;
                    SkinMesh skinMesh = block.GetComponent<SkinMesh>();
                    skinMesh.Initialize(i, j, k);
                }
            }
        }
    }
    public void SetLevel()
    {
        SetSkinMesh();
        SetPlaneBlock();
        SetRotator();
        SetSwitchBlock();
        SetHoleBlock();

        parentTransformX.rotation = Quaternion.Euler(90.0f, 90.0f, 0.0f);
        parentTransformY.rotation = Quaternion.identity;
        parentTransformZ.rotation = Quaternion.Euler(0.0f, -90.0f, 90.0f);
    }
}
