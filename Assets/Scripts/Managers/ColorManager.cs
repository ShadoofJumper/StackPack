using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color standartColor;
    private Gradient            blocksGradient;
    private GradientColorKey[]  blockColorKeys;
    private GradientAlphaKey[]  blockAlphaKeys;
    private int                 currentIdInGradiant;

    //test
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;
    public GameObject sphere4;

    private void Awake()
    {
        SetupFirstBlocksGradient();

        sphere1.GetComponent<MeshRenderer>().material.color = blocksGradient.Evaluate(0.0f);
        sphere2.GetComponent<MeshRenderer>().material.color = blocksGradient.Evaluate(0.3f);
        sphere3.GetComponent<MeshRenderer>().material.color = blocksGradient.Evaluate(0.7f);
        sphere4.GetComponent<MeshRenderer>().material.color = blocksGradient.Evaluate(1.0f);
    }

    private void SetupFirstBlocksGradient()
    {
        blocksGradient = new Gradient();
        blockColorKeys = new GradientColorKey[2];
        blockAlphaKeys = new GradientAlphaKey[2];
        Color startColor = GetRandomColorFor(standartColor);
        Color finisColor = GetRandomColorFor(startColor);
        UpdateBlocksGradientColors(startColor, finisColor);
    }

    public Color GetColorFromGradient()
    {
        currentIdInGradiant++;
        //get color
        Color colorFromGradient = blocksGradient.Evaluate(currentIdInGradiant * 0.1f);
        //update gradient colors if on end
        if (currentIdInGradiant == 10)
        {
            Debug.Log("here!!!!!!!!!!!!");
            UpdateBlocksGradientColors(colorFromGradient, GetRandomColorFor(colorFromGradient));
            sphere1.GetComponent<MeshRenderer>().material.color = blocksGradient.Evaluate(0.0f);
            sphere2.GetComponent<MeshRenderer>().material.color = blocksGradient.Evaluate(0.3f);
            sphere3.GetComponent<MeshRenderer>().material.color = blocksGradient.Evaluate(0.7f);
            sphere4.GetComponent<MeshRenderer>().material.color = blocksGradient.Evaluate(1.0f);
            currentIdInGradiant = 0;
        }
        return colorFromGradient;
    }

    private Gradient UpdateBlocksGradientColors(Color firstColor, Color finishColor)
    {
        blockColorKeys[0].color   = firstColor;
        blockColorKeys[0].time    = 0.0f;
        blockColorKeys[1].color   = finishColor;
        blockColorKeys[1].time    = 1.0f;
        blockAlphaKeys[0].alpha   = 1.0f;
        blockAlphaKeys[0].time    = 0.0f;

        blocksGradient.SetKeys(blockColorKeys, blockAlphaKeys);
        return blocksGradient;
    }

    /// get random color for set color, using this for keep same color style
    /// and color cant repeat
    private Color GetRandomColorFor(Color colorFrom)
    {
        //convert to hsv
        float H, S, V;
        Color.RGBToHSV(colorFrom, out H, out S, out V);
        //add to hue random step
        H += Random.Range(90, 150) / 360.0f;
        H = H % 1.0f;
        S = Random.Range(0.6f, 1.0f);
        return Color.HSVToRGB(H, S, V);
    }

    private Color GetOppositColor(Color color)
    {
        //convert to hsv
        float H, S, V;
        Color.RGBToHSV(color, out H, out S, out V);
        //get hsv opposit color
        H = (H + 0.5f) % 1.0f;
        return Color.HSVToRGB(H, S, V);
    }
}
