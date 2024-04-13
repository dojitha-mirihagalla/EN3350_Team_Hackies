// using UnityEngine;
// using System.Collections;
// using UnityEngine.Networking; // Required for UnityWebRequest

// public class PlayerAPIManager : MonoBehaviour
// {
//     private string serverUrl = "http://20.15.114.131:8080/api"; // Change this to your server URL
//     private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM3OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZA"; // Change this to your API key
//     private string authToken; // Stores the JWT token after authentication


//     void Start()
//     {
//         // Start the authentication process
//         StartCoroutine(AuthenticatePlayer());
//     }

//     IEnumerator AuthenticatePlayer()
//     {
//         // Construct the request body for authentication
//         string requestBody = "{\"apiKey\": \"" + apiKey + "\"}";

//         // Construct the UnityWebRequest for authentication
//         var authRequest = new UnityWebRequest(serverUrl + "/login", "POST");
//         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
//         authRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
//         authRequest.downloadHandler = new DownloadHandlerBuffer();
//         authRequest.SetRequestHeader("Content-Type", "application/json");

//         // Send the web request asynchronously and wait for the response
//         yield return authRequest.SendWebRequest();

//         // Check if the authentication request was successful
//         if (authRequest.result != UnityWebRequest.Result.Success)
//         {
//             Debug.LogError("Authentication Error: " + authRequest.error);
//             yield break; // Exit coroutine if authentication fails
//         }

//         // Parse the JSON response to get the JWT token
//         string jsonResponse = authRequest.downloadHandler.text;
//         AuthenticationResponse response = JsonUtility.FromJson<AuthenticationResponse>(jsonResponse);
//         authToken = response.token;

//         Debug.Log("Authentication Successful. Token: " + authToken);

//         // Once authenticated, fetch the player profile
//         StartCoroutine(FetchPlayerProfile());
//     }

//     IEnumerator FetchPlayerProfile()
//     {
//         // Construct the UnityWebRequest to fetch player profile
//         var profileRequest = UnityWebRequest.Get(serverUrl + "/user/profile/view");

//         // Set the authorization token in the request header
//         profileRequest.SetRequestHeader("Authorization", "Bearer " + authToken);

//         // Send the web request asynchronously and wait for the response
//         yield return profileRequest.SendWebRequest();

//         // Check if the profile request was successful
//         if (profileRequest.result != UnityWebRequest.Result.Success)
//         {
//             Debug.LogError("Error fetching player profile: " + profileRequest.error);
//             yield break; // Exit coroutine if fetching profile fails
//         }

//         // Parse the JSON response to get player profile data
//         string jsonResponse = profileRequest.downloadHandler.text;
//         PlayerProfile profile = JsonUtility.FromJson<PlayerProfile>(jsonResponse);

//         // Log the player profile information
//         Debug.Log("Player Profile - FirstName: " + profile.user.firstname +
//                   ", LastName: " + profile.user.lastname +
//                   ", Username: " + profile.user.username +
//                   ", NIC: " + profile.user.nic +
//                   ", Phone Number: " + profile.user.phoneNumber +
//                   ", Email: " + profile.user.email);

//         // Handle other profile information as needed
//     }

//     // Define a class to represent the authentication response
//     [System.Serializable]
//     private class AuthenticationResponse
//     {
//         public string token;
//     }

//     // Define a class to represent the player profile
//     [System.Serializable]
//     private class PlayerProfile
//     {
//         public User user;
//     }

//     [System.Serializable]
//     private class User
//     {
//         public string firstname;
//         public string lastname;
//         public string username;
//         public string nic;
//         public string phoneNumber;
//         public string email;
//     }
// }
