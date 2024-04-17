using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginButtonController : MonoBehaviour
{
    public void GoToProfilePage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GetDataScne");
    }
}
