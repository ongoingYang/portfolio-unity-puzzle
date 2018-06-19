using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMesh : Block {

    [SerializeField]
    private GameObject skinMeshPrefab;
    [SerializeField]
    private Vector3 meshOffset;

    private GameObject skinMesh;
    public override void Initialize(int i, int j, int k)
    {
        skinMesh = Instantiate(skinMeshPrefab, meshOffset, Quaternion.identity, transform) as GameObject;
        this.transform.position = new Vector3(IndexToNegative(i), IndexToNegative(j), IndexToPositive(k));
        this.transform.SetParent(LevelDesigner.skinMeshTransform);
    }
}
