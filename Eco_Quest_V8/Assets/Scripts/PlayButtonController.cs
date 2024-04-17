using UnityEngine;
using UnityEngine.SceneManagement;
using Proyecto26;
using System.Collections;
using UnityEngine.Networking;

public class PlayButton : MonoBehaviour
{
    public bool isQuizDone = false ; // Assume initially not done
    public string database_url = "https://hackies-questionnaire-default-rtdb.asia-southeast1.firebasedatabase.app/";
    public int score = 0;

    private string serverUrl = "http://20.15.114.131:8080/api"; // Change this to your server URL
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM3OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZA"; // Change this to your API key
    private string authToken; // Stores the JWT token after authentication

    User user_state = new User();

    private void Start()
    {
        // Start the authentication process when the game starts
        StartCoroutine(AuthenticatePlayer());
    }

    public void PlayGame()
    {
        

        StartCoroutine(PlayGameRoutine());
    }

    private IEnumerator PlayGameRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckQuizStatus());
        if (!isQuizDone)
        {
            string externalURL = "http://localhost:3000/"; // Set your external URL here
            Application.OpenURL(externalURL);

            // Wait until isQuizDone flag turns true
            while (!isQuizDone)
            {
                StartCoroutine(CheckQuizStatus());
                yield return new WaitForSeconds(0.1f); // Adjust the interval as needed
                
                
            }

            Debug.Log("Quiz is done. Loading game scene...");
            SceneManager.LoadScene("GamePlay");
        }
        else
        {
            Debug.Log("Quiz is already done. Loading game scene...");
            SceneManager.LoadScene("GamePlay");
        }
    }


    private IEnumerator AuthenticatePlayer()
    {
        // Construct the request body for authentication
        string requestBody = "{\"apiKey\": \"" + apiKey + "\"}";

        // Construct the UnityWebRequest for authentication
        var authRequest = new UnityWebRequest(serverUrl + "/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
        authRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        authRequest.downloadHandler = new DownloadHandlerBuffer();
        authRequest.SetRequestHeader("Content-Type", "application/json");

        // Send the web request asynchronously and wait for the response
        yield return authRequest.SendWebRequest();

        // Check if the authentication request was successful
        if (authRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Authentication Error: " + authRequest.error);
            yield break; // Exit coroutine if authentication fails
        }

        // Parse the JSON response to get the JWT token
        string jsonResponse = authRequest.downloadHandler.text;
        AuthenticationResponse response = JsonUtility.FromJson<AuthenticationResponse>(jsonResponse);
        authToken = response.token;

        Debug.Log("Authentication Successful. Token: " + authToken);

        // Once authenticated, fetch the player profile
        StartCoroutine(FetchPlayerProfile());
    }

    private IEnumerator FetchPlayerProfile()
    {
        // Construct the UnityWebRequest to fetch player profile
        var profileRequest = UnityWebRequest.Get(serverUrl + "/user/profile/view");

        // Set the authorization token in the request header
        profileRequest.SetRequestHeader("Authorization", "Bearer " + authToken);

        // Send the web request asynchronously and wait for the response
        yield return profileRequest.SendWebRequest();

        // Check if the profile request was successful
        if (profileRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching player profile: " + profileRequest.error);
            yield break; // Exit coroutine if fetching profile fails
        }

        // Parse the JSON response to get player profile data
        string jsonResponse = profileRequest.downloadHandler.text;
        PlayerProfile profile = JsonUtility.FromJson<PlayerProfile>(jsonResponse);

        user_state.firstname = profile.user.firstname;
        user_state.lastname = profile.user.lastname;
        user_state.username = profile.user.username;
        user_state.nic = profile.user.nic;
        user_state.phoneNumber = profile.user.phoneNumber;
        user_state.email = profile.user.email;

        // Start checking quiz status after fetching profile
        StartCoroutine(CheckQuizStatus());
    }

    private IEnumerator CheckQuizStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f); // Adjust the interval as needed

            // Fetch data from Firebase
            yield return StartCoroutine(FetchQuizData());

            // Check if the quiz is done and load the game scene only if the current scene is not "GamePlay"
            if (isQuizDone && SceneManager.GetActiveScene().name != "GamePlay")
            {
                //SceneManager.LoadScene("GamePlay");
                //Debug.Log("Loading game scene...");
            }
        }
    }




    private IEnumerator FetchQuizData()
    {
        string dataUrl = database_url + user_state.username + ".json";
        RestClient.Get<QuizData>(dataUrl).Then(response =>
        {
            if (response != null)
            {
                isQuizDone = response.isQuizDone;
                score = response.score;

                Debug.Log("Fetched Data - isQuizDone: " + isQuizDone + ", Score: " + score);
            }
            else
            {
                Debug.LogError("Failed to fetch data. Response is null.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Error fetching data: " + error.Message);
        });

        yield return null;
    }

    [System.Serializable]
    private class User
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;
    }

    [System.Serializable]
    private class AuthenticationResponse
    {
        public string token;
    }

    [System.Serializable]
    private class PlayerProfile
    {
        public User user;
    }

    [System.Serializable]
    public class QuizData
    {
        public bool isQuizDone;
        public int score;
    }
}