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
    private int         currentLevel;
    private Vector3     currentLevelCenter;
    private Vector3     lastBlockSize;
    private BlockInfo   currentBlock;
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
        currentLevel = 1;
        SpawnNewLevelBlock();
    }

    // ------------------ Drop block logic -------------------

    public void DropBlock()
    {
        //stop move
        currentBlock.mover.StopMove();
        //get offset
        Vector3 offset = GetDropOffset(currentBlock.gameObject);
        if (offset.magnitude <= hitAccuracy)
        {
            CompleteStage();
        }
        else
        {
            blockCrafter.SplitBlock(currentBlock.gameObject, offset.x);
        }
    }

    private Vector3 GetDropOffset(GameObject blockObject)
    {
        return blockObject.transform.position - currentLevelCenter;
    }

    private void CompleteStage()
    {
        Debug.Log("Prefect!");
        //move block to center
        currentBlock.gameObject.transform.position = currentLevelCenter;
    }

    // ---------------- Blocks creating logic ----------------
    private void SpawnNewLevelBlock()
    {
        Vector3 randomSide  = moveDirections[1];//Random.Range(0, moveDirections.Length)
        //increas level height
        currentLevelCenter  = Vector3.up * levelHeight;
        //create block
        BlockMover mover    = CreateMovingBlock(randomSide);
        mover.StartMove();
    }

    private BlockMover CreateMovingBlock(Vector3 moveDirection)
    {
        //create block
        Vector3 newSize             = lastBlockSize != Vector3.zero ? lastBlockSize 
            : new Vector3(1.0f, levelHeight, 1.0f);
        Color newColor              = Color.blue;
        GameObject newBlock         = blockCrafter.CreateBlock(newSize, newColor);
        newBlock.name = "Block_" + currentLevel;
        //place on start
        newBlock.transform.position = currentLevelCenter + moveDirection * blockStartOffset;
        //add moving component to block
        BlockMover mover            = newBlock.AddComponent<BlockMover>();
        mover.Construct(moveDirection, blockLoopDelta, blocksSpeed);
        // save info
        currentBlock                = new BlockInfo(newBlock, mover, moveDirection);
        return mover;
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
        public BlockInfo(GameObject _gameObject, BlockMover _blockMover, Vector3 _moveDirection)
        {
            gameObject      = _gameObject;
            mover           = _blockMover;
            moveDirection   = _moveDirection;
        }

    }
}
