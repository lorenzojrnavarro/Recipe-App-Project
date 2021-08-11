using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private string desiredScene;

    public void ChangeScene()
    {
        SceneManager.LoadScene(desiredScene);
    }
}
