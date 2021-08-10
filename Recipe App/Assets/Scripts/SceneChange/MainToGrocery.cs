using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainToGrocery : MonoBehaviour
{
    public void goGrocery()
    {
        SceneManager.LoadScene(2);
    }
}
