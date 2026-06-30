using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseManager : Singleton<MouseManager>
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch(scene.buildIndex)
        {
            case 2: SetMouse(false);
                break;
            default: SetMouse(true);
                break;
        }
    }

    public void SetMouse(bool state)
    {
        Cursor.visible = state;

        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
    }
}
