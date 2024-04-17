using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardButtonControlle : MonoBehaviour
{
    public void OpenLeaderboard()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Leaderboard");
    }
}
