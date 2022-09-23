using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    public int player1Score = 0;
    public int player2Score = 0;

    public Text p1Text;
    public Text p2Text;

    public void AddP1Score()
    {
        player1Score++;
            p1Text.text = "Score: " + player1Score.ToString();
    }

    public void AddP2Score()
    {
        player2Score++;
        p2Text.text = "Score: " + player2Score.ToString();
    }
}
