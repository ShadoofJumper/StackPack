using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneController : MonoBehaviour
{
    // game configs
    [SerializeField] private float  blockLoopDelta;
    [SerializeField] private float  blockStartOffset;
    [SerializeField] private float  levelHeight;
    [SerializeField] private float  blocksSpeed;
    [SerializeField] private float  hitAccuracy;
    // in game logic var
    private int                 currentLevel;
    private Vector3             currentLevelCenter;
    private BlockTransformInfo  lastBlockInfo;
    private BlockInfo           currentBlock;

    private readonly Vector3[] moveDirections = new Vector3[]
{
        Vector3.forward,
        Vector3.right,
        Vector3.left,
        Vector3.back,
    };
    // dependency
    private BlockCrafter blockCrafter;

    [Inject]
    private void Construct(BlockCrafter blockCrafter)
    {
        this.blockCrafter = blockCrafter;
    }

    private void Start()
    {
        StartNewStage();
    }

    // ------------------ Stage logic ------------------------

    private void StartNewStage()
    {
        currentLevel += 1;
        SpawnNewLevelBlock();
    }


    // ------------------ Drop block logic -------------------
    public void DropBlock()
    {
        currentBlock.mover.StopMove();
        Vector3 offset          = GetDropOffset(currentBlock.gameObject);
        bool isHorizontal       = offset.z == 0.0f ? true : false;
        float offsetAlonAxis    = isHorizontal ? offset.x : offset.z;

        if (offset.magnitude <= hitAccuracy)
        {
            PrefectStage();
        }
        else if (CheckFailDrop(offsetAlonAxis, isHorizontal))
        {
            FailDrop();
        }
        else
        {
            SliceBlock(offsetAlonAxis, isHorizontal);
        }
    }

    private bool CheckFailDrop(float offsetAlonAxis, bool isHorizontal)
    {
        float blockSliceSide = isHorizontal ? currentBlock.block.SideA : currentBlock.block.SideB;
        return Mathf.Abs(offsetAlonAxis) > blockSliceSide;
    }

    private Vector3 GetDropOffset(GameObject blockObject)
    {
        return blockObject.transform.position - currentLevelCenter;
    }

    private void SliceBlock(float offsetAlongAxis, bool isHorizontal)
    {
        GameObject[] gameObjects = blockCrafter.SplitBlock(currentBlock.gameObject, offsetAlongAxis, isHorizontal);
        //enable rigidbody to not stable part
        gameObjects[0].GetComponent<Rigidbody>().isKinematic = false;
        //save last block size
        lastBlockInfo = new BlockTransformInfo(gameObjects[1].transform.localScale, gameObjects[1].transform.position);
        StartNewStage();
    }

    private void PrefectStage()
    {
        Debug.Log("Prefect!");
        //move block to center
        currentBlock.gameObject.transform.position = currentLevelCenter;
        StartNewStage();
    }

    private void FailDrop()
    {
        Debug.Log("Fail!!");
        currentBlock.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    // ---------------- Blocks creating logic ----------------
    private void SpawnNewLevelBlock()
    {
        Vector3 randomSide  = moveDirections[Random.Range(0, moveDirections.Length)];
        //set center
        currentLevelCenter = lastBlockInfo.position;
        //increas level height
        currentLevelCenter += Vector3.up * levelHeight;
        //create block
        BlockMover mover    = CreateMovingBlock(randomSide);
        mover.StartMove();
    }

    private BlockMover CreateMovingBlock(Vector3 moveDirection)
    {
        //create block
        Vector3 newSize             = lastBlockInfo.scale != Vector3.zero ? lastBlockInfo.scale
            : new Vector3(1.0f, levelHeight, 1.0f);
        Color newColor              = GetRandomBlockColor();
        GameObject newBlock         = blockCrafter.CreateBlock(newSize, newColor);
        newBlock.name = "Block_" + currentLevel;
        //place on start
        newBlock.transform.position = currentLevelCenter + moveDirection * blockStartOffset;
        //add moving component to block
        BlockMover mover            = newBlock.AddComponent<BlockMover>();
        mover.Construct(moveDirection, blockLoopDelta, blocksSpeed, currentLevelCenter);//
        // save info
        currentBlock                = new BlockInfo(newBlock, mover, moveDirection);
        return mover;
    }

    //stub
    private Color GetRandomBlockColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    // --------------------------------------------------------

    // ------------------------- DEV --------------------------
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //loop points
        Gizmos.color = Color.green;
        foreach (Vector3 direction in moveDirections)
        {
            Vector3 spot = Vector3.zero + direction * blockLoopDelta;
            Gizmos.DrawWireSphere(spot, 0.05f);
        }
        //start points
        Gizmos.color = Color.red;
        foreach (Vector3 direction in moveDirections)
        {
            Vector3 spot = Vector3.zero + direction * blockStartOffset;
            Gizmos.DrawWireSphere(spot, 0.05f);
        }
    }
#endif

    //struct to save info about block
    public struct BlockInfo
    {
        public GameObject   gameObject;
        public BlockMover   mover;
        public Vector3      moveDirection;
        public Block        block;

        public BlockInfo(GameObject _gameObject, BlockMover _blockMover, Vector3 _moveDirection)
        {
            gameObject      = _gameObject;
            mover           = _blockMover;
            moveDirection   = _moveDirection;
            block           = _gameObject.GetComponent<Block>();
        }

    }

    //struct to save info about last block transform
    public struct BlockTransformInfo
    {
        public Vector3 scale;
        public Vector3 position;

        public BlockTransformInfo(Vector3 _scale, Vector3 _position)
        {
            scale       = _scale;
            position    = _position;
        }

    }
}
