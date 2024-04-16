using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfileButtonController : MonoBehaviour
{
    public void OpenPlayerProfile()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GetDataScne");
    }
}
