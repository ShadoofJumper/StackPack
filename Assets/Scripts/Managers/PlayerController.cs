using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class PlayerController : MonoBehaviour
{
    private SceneController sceneController;

    [Inject]
    private void Construct(SceneController sceneController)
    {
        this.sceneController = sceneController;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sceneController.DropBlock();
        }
    }
}
