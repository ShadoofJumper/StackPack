using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Block : MonoBehaviour
{
    private float sideA;
    private float sideB;
    private float height;
    private Color color;
    private Material blockMaterial;

    #region Properties
    public float SideA {
        get { return sideA; }
        set {
            sideA = value;
            UpdateBlockView();
        }
    }
    public float SideB
    {
        get { return sideB; }
        set
        {
            sideB = value;
            UpdateBlockView();
        }
    }
    public float Height
    {
        get { return height; }
        set
        {
            height = value;
            UpdateBlockView();
        }
    }
    public Color Color
    {
        get { return color; }
        set
        {
            color = value;
            UpdateBlockView();
        }
    }
    #endregion

    private void Awake()
    {
        blockMaterial = GetComponent<MeshRenderer>().material;
    }

    public void CustomizeBlock(float sideA, float sideB, float height, Color blockColor)
    {
        this.sideA  = sideA;
        this.sideB  = sideB;
        this.color  = blockColor;
        this.height = height;
        UpdateBlockView();
    }

    private void UpdateBlockView()
    {
        transform.localScale = new Vector3(sideA, height, sideB);
        blockMaterial.color  = color;
    }
}
