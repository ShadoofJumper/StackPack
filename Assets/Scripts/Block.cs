using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Block : MonoBehaviour
{
    private float   sideA;
    private float   sideB;
    private float   height;
    private Color   blockColor;
    private Material blockMaterial;

    private void Awake()
    {
        blockMaterial = GetComponent<MeshRenderer>().material;
    }

    public void Input(float sideA, float sideB, float height, Color blockColor)
    {
        Debug.Log("Input");
        this.sideA      = sideA;
        this.sideB      = sideB;
        this.blockColor = blockColor;
        this.height     = height;

        CustomizeBlock();
    }

    private void CustomizeBlock()
    {
        transform.localScale = new Vector3(sideA, height, sideB);
        blockMaterial.color  = blockColor;
    }
}
