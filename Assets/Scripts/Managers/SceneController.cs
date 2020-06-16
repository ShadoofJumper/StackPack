using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject blockOrigin;
    [SerializeField] private Transform blocksFolder;

    private void Start()
    {
        CreateMovingBlock(Vector3.zero);
    }

    private void CreateMovingBlock(Vector3 position)
    {
        GameObject newBlock = Instantiate(blockOrigin, blocksFolder);
        Block block         = newBlock.GetComponent<Block>();
        block.Input(1.0f, 1.0f, 0.2f, Color.black);
        newBlock.transform.position = position;
    }

}
