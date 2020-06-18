using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class PlayerController : MonoBehaviour
{
    private SceneController sceneController;
    private GameManager     gameManager;

    [Inject]
    private void Construct(SceneController sceneController, GameManager gameManager)
    {
        this.sceneController    = sceneController;
        this.gameManager        = gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameManager.IsGameInProgress)
        {
            sceneController.DropBlock();
        }
    }
}
