using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void FreeRoam()
    {
        SceneManager.LoadScene("FreeRoam");
    }
    public void SinglePlayer()
    {
        SceneManager.LoadScene("SingleplayerUI");
    }
}
