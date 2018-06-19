using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaneBlock : Block {

    Renderer rend;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        Debug.Log(rend.materials.Length);
        Debug.Log(rend.materials[0].color);
        Debug.Log(rend.materials[1].color);
        Debug.Log(rend.materials[2].color);
        
    }

    public override void Initialize(int origin, int j, int k){

        rend.material.color = rend.materials[origin].color;
        base.Initialize(origin, j, k);
        
        
    }
}
