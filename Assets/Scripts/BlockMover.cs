using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockMover : MonoBehaviour
{
    [SerializeField] private float movingDelta = 0.5f;
    [SerializeField] private float speed = 1.0f;
    private Vector3 moveDirection;
    private bool    isMovingLoop;
    private Vector3 center;
    private Vector3 currentPos;
    private float   startLoopTime;

    public void Construct(Vector3 moveDirection, float movingDelta, float speed)
    {
        this.moveDirection  = moveDirection;
        this.movingDelta    = movingDelta;
        this.speed          = speed;
    }

    public void StartMove()
    {
        //set block center
        center = Vector3.up * transform.position.y;
        //move to center
        Vector3 loopPointStart = center;
        StartCoroutine(MoveToCenter());
    }

    public void StopMove()
    {
        StopAllCoroutines();
        isMovingLoop = false;
    }

    private void Update()
    {
        if (isMovingLoop)
        {
            MoveInLoop();
        }
    }

    IEnumerator MoveToCenter()
    {
        while (Vector3.Distance(transform.position, center) > 0.001f)
        {
            float step          = speed * Time.deltaTime;
            currentPos          = Vector3.MoveTowards(transform.position, center, step);
            transform.position  = currentPos;
            yield return null;
        }
        startLoopTime   = Time.time;
        isMovingLoop    = true;
    }

    private void MoveInLoop()
    {
        float step = (Time.time - startLoopTime) * speed;
        currentPos = center + moveDirection * -1 * movingDelta * Mathf.Sin(step);
        transform.position = currentPos;
    }


}