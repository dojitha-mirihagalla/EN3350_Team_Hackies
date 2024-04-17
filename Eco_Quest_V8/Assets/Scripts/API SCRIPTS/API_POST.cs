using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;
using EasyUI.Popup;

public class PlayerAPIManagerPOST : MonoBehaviour
{
    private string serverUrl = "http://20.15.114.131:8080/api";
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM3OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZA";
    private string authToken;
    private bool AuthenticationSuccessful = false;
    private User user_state = new User();

    // References to input fields in the UI
    public TMP_InputField firstnameInput;
    public TMP_InputField lastnameInput;
    public TMP_InputField nicInput;
    public TMP_InputField phoneNumberInput;
    public TMP_InputField emailInput;


    void Start()
    {
        StartCoroutine(AuthenticatePlayer());
    }

    public void btn_action_when_pressed_gotomainmenu(){
        StartCoroutine(FetchPlayerProfile());
        // check if the user details are null or not
        if (user_state.firstname == null || user_state.lastname == null || user_state.username == null || user_state.nic == null || user_state.phoneNumber == null || user_state.email == null){
            Popup.Show("Oops!", "Please fill in the incomplete data", "OK", PopupColor.Green);
        }
        else{
            //load the main menu scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator AuthenticatePlayer()
    {
        string requestBody = "{\"apiKey\": \"" + apiKey + "\"}";
        var authRequest = new UnityWebRequest(serverUrl + "/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
        authRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        authRequest.downloadHandler = new DownloadHandlerBuffer();
        authRequest.SetRequestHeader("Content-Type", "application/json");

        yield return authRequest.SendWebRequest();

        if (authRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Authentication Error: " + authRequest.error);
            Popup.Show("Error", "Error authenticating player:" + authRequest.error, "Retry", PopupColor.Green);
            yield break;
        }

        string jsonResponse = authRequest.downloadHandler.text;
        AuthenticationResponse response = JsonUtility.FromJson<AuthenticationResponse>(jsonResponse);
        authToken = response.token;

        Debug.Log("Authentication Successful. Token: " + authToken);
        AuthenticationSuccessful = true;
    }




    public void UpdatePlayerProfileFromInput()
    {
        
        try{
             StartCoroutine(AuthenticatePlayer());
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error authenticating player: ---> " + e.Message);
            Popup.Show("Error", "Error authenticating player: --->" + e.Message, "Retry", PopupColor.Green);

        }


        // Get values from input fields
        string newFirstname = firstnameInput.text;
        string newLastname = lastnameInput.text;
        string newNIC = nicInput.text;
        string newPhoneNumber = phoneNumberInput.text;
        string newEmail = emailInput.text;


        // Validation checks
        bool isDataValid = true;
        

        // Check if first name is not empty
        if (string.IsNullOrEmpty(newFirstname))
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "First name cannot be empty", "OK", PopupColor.Green);
            Debug.Log("First name cannot be empty");
        }

        // Check if last name is not empty
        else if (string.IsNullOrEmpty(newLastname))
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "Last name cannot be empty", "OK", PopupColor.Green);
            Debug.Log("Last name cannot be empty");
        }
        // check if NIC is not empty and has exactly 12 characters
        else if (string.IsNullOrEmpty(newNIC) || newNIC.Length != 12)
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "Invalid NIC", "OK", PopupColor.Green);
            Debug.Log("Invalid NIC format. Please enter a valid NIC number.");
        }
    
        // Check if phone number is not empty and has exactly 10 digits
        else if (string.IsNullOrEmpty(newPhoneNumber) || newPhoneNumber.Length != 10 || !int.TryParse(newPhoneNumber, out _))
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "Invalid phone number", "OK", PopupColor.Green);
            Debug.Log("Invalid phone number format. Please enter a valid phone number.");
        }

        // Check if email is not empty and has correct format
        else if (string.IsNullOrEmpty(newEmail) || !Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "Invalid email", "OK", PopupColor.Green);
            Debug.Log("Invalid email format. Please enter a valid email address.");
        }

        // If all data is valid and authentication is successful, update the player profile and display the Corresponding message

        else if (AuthenticationSuccessful)
        {
            try
            {
                StartCoroutine(UpdateProfileCoroutine(newFirstname, newLastname, newNIC, newPhoneNumber, newEmail));
            }
            catch (System.Exception e)
            {
               Popup.Show("Error", e.Message, "Retry", PopupColor.Green);
            }
        }
        else
        {
    
            Popup.Show("Error", "Authentication failed.", "Retry", PopupColor.Green);
        }

                 
    }



    IEnumerator UpdateProfileCoroutine(string newFirstname, string newLastname, string newNIC, string newPhoneNumber, string newEmail)
    {
        string requestBody = "{\"firstname\": \"" + newFirstname + "\", " +
                             "\"lastname\": \"" + newLastname + "\", " +
                             "\"nic\": \"" + newNIC + "\", " +
                             "\"phoneNumber\": \"" + newPhoneNumber + "\", " +
                             "\"email\": \"" + newEmail + "\"}";

        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
         
        using (UnityWebRequest request = UnityWebRequest.Put(serverUrl + "/user/profile/update", bodyRaw))
        {
            request.method = UnityWebRequest.kHttpVerbPUT;
            request.SetRequestHeader("Authorization", "Bearer " + authToken);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error updating player profile: " + request.error);
                
                yield break;
            }

            Debug.Log("Player profile updated successfully");
            Popup.Show("Success", "Your account updated successfully.", "OK", PopupColor.Green);
        }
    }

        IEnumerator FetchPlayerProfile()
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

        // set the user details to check if they are null or not
        user_state.firstname = profile.user.firstname;
        user_state.lastname = profile.user.lastname;
        user_state.username = profile.user.username;
        user_state.nic = profile.user.nic;
        user_state.phoneNumber = profile.user.phoneNumber;
        user_state.email = profile.user.email;
                
    }


    [System.Serializable]
    private class AuthenticationResponse
    {
        public string token;
    }

    // Define a class to represent the player profile
    [System.Serializable]
    private class PlayerProfile
    {
        public User user;
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
}
