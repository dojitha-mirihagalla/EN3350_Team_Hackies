using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class API_call : MonoBehaviour
{
    
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM3OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZA";
    private string serverUrl = "http://20.15.114.131:8080/api/login"; // Replace with your server's URL

    public void SendHttpRequestAndSwitchScene()
    {
        StartCoroutine(SendRequestAndSwitchScene());
    }

    private IEnumerator SendRequestAndSwitchScene()
    {
        // Create request body.
        string requestBody = "{\"apiKey\": \"" + apiKey + "\"}";

        // Create request
        var request = new UnityWebRequest(serverUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send request
        yield return request.SendWebRequest();

        Debug.Log("Response Code: " + request.responseCode);

        // Handle response
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;

            // Switch scene after receiving response
            SceneManager.LoadScene("Player_profile");

        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}
