using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCrafter : MonoBehaviour
{
    [SerializeField] private GameObject blockOrigin;
    [SerializeField] private Transform  blocksFolder;


    public GameObject[] SplitBlock(GameObject blockObject, float offset, bool isHorizontal, Vector3 center)
    {
        Block originalBlock = blockObject.GetComponent<Block>();
        float slicedSide    = isHorizontal ? originalBlock.SideA : originalBlock.SideB;
        //slice axis of block center, need for correct new blocks position
        float centerAxis    = isHorizontal ? center.x : center.z;

        SliceInfo sliceInfo = CalculaterNewSlicedParams(offset, slicedSide, centerAxis);
        Vector3 oldBlockPos = blockObject.transform.position;
        Vector3 newBlockPos = blockObject.transform.position;
        Vector3 newBlockSize = new Vector3(originalBlock.SideA, originalBlock.Height, originalBlock.SideB);

        if (isHorizontal)
        {
            originalBlock.SideA = sliceInfo.leftPartLength;
            oldBlockPos.x   = sliceInfo.rightPartPos;
            newBlockPos.x   = sliceInfo.leftPartPos;
            newBlockSize.x  = sliceInfo.rightPartLength;
        }
        else
        {
            originalBlock.SideB = sliceInfo.leftPartLength;
            oldBlockPos.z   = sliceInfo.rightPartPos;
            newBlockPos.z   = sliceInfo.leftPartPos;
            newBlockSize.z  = sliceInfo.rightPartLength;
        }
        //create new block
        GameObject newBlockObject = CreateBlock(newBlockSize, originalBlock.Color);
        newBlockObject.name = blockObject.name + "_part";
        //set positions
        blockObject.transform.position = oldBlockPos;
        newBlockObject.transform.position = newBlockPos;

        return new GameObject[] { blockObject , newBlockObject };
    }

    private SliceInfo CalculaterNewSlicedParams(float offsetFromCenter, float sliceSideLenght, float centerAxis)
    {
        //calculate new blocks sizes
        float fullLenght        = sliceSideLenght;
        float leftPartLenght    = Mathf.Abs(offsetFromCenter);
        float rightPartLength   = sliceSideLenght - leftPartLenght;

        //calculate new blocks positions
        float spliteSign    = offsetFromCenter / Mathf.Abs(offsetFromCenter);
        float edge          = sliceSideLenght / 2;
        float newPos1 = (edge - rightPartLength / 2) * spliteSign + centerAxis;
        float newPos2 = (edge + leftPartLenght  / 2) * spliteSign + centerAxis;

        return new SliceInfo(leftPartLenght, rightPartLength, newPos1, newPos2);
    }

    public GameObject CreateBlock(Vector3 size, Color blockColor)
    {
        GameObject newBlock = Instantiate(blockOrigin, blocksFolder);
        Block block         = newBlock.GetComponent<Block>();
        block.CustomizeBlock(size.x, size.z, size.y, blockColor);
        return newBlock;
    }


    //struct to save info about block
    public struct SliceInfo
    {
        public float leftPartLength;
        public float rightPartLength;
        public float leftPartPos;
        public float rightPartPos;

        public SliceInfo(float _leftPartLength, float _rightPartLength, float _leftPartPos, float _rightPartPos)
        {
            leftPartLength  = _leftPartLength;
            rightPartLength = _rightPartLength;
            leftPartPos     = _leftPartPos;
            rightPartPos    = _rightPartPos;
        }

    }
}
