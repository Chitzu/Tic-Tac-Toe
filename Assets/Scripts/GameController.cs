using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;

}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;

    public GameObject gameOverPanel;
    public Text gameOverText;

    private int moveCount;
    public bool gameAlreadyOver;

    public GameObject restartButton;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;

    public GameObject WinBar1;
    public GameObject WinBar2;
    public GameObject WinBar3;
    public GameObject WinBar4;
    public GameObject WinBar5;
    public GameObject WinBar6;
    public GameObject WinBar7;
    public GameObject WinBar8;

    void Awake()
    {
        gameOverPanel.SetActive(false);
        SetGameControllerReferenceOnButtons();
        moveCount = 0;
        restartButton.SetActive(false);

        WinBar1.SetActive(false);
        WinBar2.SetActive(false);
        WinBar3.SetActive(false);
        WinBar4.SetActive(false);
        WinBar5.SetActive(false);
        WinBar6.SetActive(false);
        WinBar7.SetActive(false);
        WinBar8.SetActive(false);

    }

    void SetGameControllerReferenceOnButtons()
    {
        for(int i = 0;i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if(playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }

        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }
    public void EndTurn()
    {
        moveCount++;
        gameAlreadyOver = false;
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            WinBar4.SetActive(true);
            GameOver(playerSide);
            gameAlreadyOver = true;
            

        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            WinBar5.SetActive(true);
            GameOver(playerSide);
            gameAlreadyOver = true;
            
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            WinBar6.SetActive(true);
            GameOver(playerSide);
            gameAlreadyOver = true;
            
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            WinBar1.SetActive(true);
            GameOver(playerSide);
            gameAlreadyOver = true;
            
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            WinBar2.SetActive(true);
            GameOver(playerSide);
            gameAlreadyOver = true;
            
        }
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            WinBar3.SetActive(true);
            GameOver(playerSide);
            gameAlreadyOver = true;
            
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            WinBar8.SetActive(true);
            GameOver(playerSide);
            gameAlreadyOver = true;
            
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            WinBar7.SetActive(true);
            GameOver(playerSide);
            gameAlreadyOver = true;
            
        }

        else if (moveCount >= 9 && !gameAlreadyOver)
        {

            GameOver("draw"); restartButton.SetActive(true);
        }
        else
        {
            ChangeSides();
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        restartButton.SetActive(true);

        if(winningPlayer == "draw")
        {
            SetGameOverText("Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
        }
    }
    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";

        if(playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX); 
        }
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        WinBar1.SetActive(false);
        WinBar2.SetActive(false);
        WinBar3.SetActive(false);
        WinBar4.SetActive(false);
        WinBar5.SetActive(false);
        WinBar6.SetActive(false);
        WinBar7.SetActive(false);
        WinBar8.SetActive(false);

        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);


        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = " ";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
   
    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}