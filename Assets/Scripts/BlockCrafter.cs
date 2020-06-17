using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCrafter : MonoBehaviour
{
    [SerializeField] private GameObject blockOrigin;
    [SerializeField] private Transform  blocksFolder;
    //test
    //private Vector3 pos1;
    //private Vector3 pos2;
    //private Vector3 edgeee;

    public GameObject[] SplitBlock(GameObject blockObject, float offset)
    {
        Block originalBlock     = blockObject.GetComponent<Block>();
        //calculate new blocks sizes
        float sideB             = originalBlock.SideB;
        float sideA2            = Mathf.Abs(offset);
        float sideA1            = originalBlock.SideA - sideA2;
        Vector3 newBlockSize    = new Vector3(sideA2, originalBlock.Height, sideB);
        //calculate new blocks positions
        float blockSpliteІign   = offset / Mathf.Abs(offset);
        float edge              = originalBlock.SideA / 2;
        float newPosX1          = (edge - sideA1 / 2) * blockSpliteІign;
        float newPosX2          = (edge + sideA2 / 2) * blockSpliteІign;
        Vector3 newPos1         = new Vector3(newPosX1, blockObject.transform.position.y, blockObject.transform.position.z);
        Vector3 newPos2         = new Vector3(newPosX2, blockObject.transform.position.y, blockObject.transform.position.z);
        //change original block
        originalBlock.SideA         = sideA1;
        //create new block
        GameObject newBlockObject   = CreateBlock(newBlockSize, originalBlock.Color);
        newBlockObject.name         = blockObject.name + "_part";
        //set positions
        blockObject.transform.position      = newPos1;
        newBlockObject.transform.position   = newPos2;
        return null;
    }

    public GameObject CreateBlock(Vector3 size, Color blockColor)
    {
        GameObject newBlock = Instantiate(blockOrigin, blocksFolder);
        Block block         = newBlock.GetComponent<Block>();
        block.CustomizeBlock(size.x, size.z, size.y, blockColor);
        return newBlock;
    }


    ////test

    //private void OnDrawGizmos()
    //{
    //    //loop points
    //    Gizmos.color = Color.yellow;

    //    Gizmos.DrawWireSphere(pos1, 0.02f);
    //    Gizmos.DrawWireSphere(pos2, 0.02f);
    //    Gizmos.DrawWireSphere(edgeee, 0.02f);
    //}
}
