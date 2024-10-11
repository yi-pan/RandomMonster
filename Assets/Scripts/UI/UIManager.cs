using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button restartButton;  // Reference to the restart button

    void Start()
    {
        // Add a listener to the button to call the GameController's Restart function
        restartButton.onClick.AddListener(() =>
        {
            GameController.GetInstance().RestartGame();
        });
    }
}
