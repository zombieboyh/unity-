using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public Canvas canv;

    public void StartGame()
    {
        canv.GetComponent<CanvasGroup>().alpha = 0;
        canv.GetComponent<CanvasGroup>().interactable = false;
        canv.GetComponent<CanvasGroup>().blocksRaycasts = false;
        SceneManager.LoadScene("Level");
    }
}
