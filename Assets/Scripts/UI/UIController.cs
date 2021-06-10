using MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Observer;
using System;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject EndGameBoard;
    // Start is called before the first frame update
    void Awake()
    {
        this.RegisterListener(EventID.OnGameLose, (o) => EndGameHandle(false));
        EndGameBoard.GetComponentInChildren<Button>().onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void EndGameHandle(bool isWin)
    {
        var canvasGroup = this.EndGameBoard.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

    }
}
