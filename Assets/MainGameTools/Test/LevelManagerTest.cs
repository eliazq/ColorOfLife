using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManagerTest : MonoBehaviour
{


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            LevelManager.LoadLevel(LevelManager.Level.StartCutScene);
        }
    }
}
