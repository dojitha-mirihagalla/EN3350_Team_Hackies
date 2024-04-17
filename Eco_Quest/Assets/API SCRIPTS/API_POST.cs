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
            yield break;
        }

        string jsonResponse = authRequest.downloadHandler.text;
        AuthenticationResponse response = JsonUtility.FromJson<AuthenticationResponse>(jsonResponse);
        authToken = response.token;

        Debug.Log("Authentication Successful. Token: " + authToken);
    }




    public void UpdatePlayerProfileFromInput()
    {
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
            Popup.Show("Error", "First name cannot be empty", "OK", PopupColor.Red);
            Debug.Log("First name cannot be empty");
        }

        // Check if last name is not empty
        else if (string.IsNullOrEmpty(newLastname))
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "Last name cannot be empty", "OK", PopupColor.Red);
            Debug.Log("Last name cannot be empty");
        }
        // check if NIC is not empty and has exactly 12 characters
        else if (string.IsNullOrEmpty(newNIC) || newNIC.Length != 12)
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "Invalid NIC", "OK", PopupColor.Red);
            Debug.Log("Invalid NIC format. Please enter a valid NIC number.");
        }
    
        // Check if phone number is not empty and has exactly 10 digits
        else if (string.IsNullOrEmpty(newPhoneNumber) || newPhoneNumber.Length != 10 || !int.TryParse(newPhoneNumber, out _))
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "Invalid phone number", "OK", PopupColor.Red);
            Debug.Log("Invalid phone number format. Please enter a valid phone number.");
        }

        // Check if email is not empty and has correct format
        else if (string.IsNullOrEmpty(newEmail) || !Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            isDataValid = false;
            // Optionally provide feedback to the user about the error
            Popup.Show("Error", "Invalid email", "OK", PopupColor.Red);
            Debug.Log("Invalid email format. Please enter a valid email address.");
        }

                 

        if (isDataValid)
        {
            try{
                StartCoroutine(UpdateProfileCoroutine(newFirstname, newLastname, newNIC, newPhoneNumber, newEmail));
                Popup.Show("Success", "Your account updated successfully.", "OK", PopupColor.Green);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error updating player profile: " + e.Message);
                Popup.Show("Error", "Error updating player profile: " + e.Message, "OK", PopupColor.Red);
            }
        }
        else
        {
            Debug.LogError("Invalid data entered. Please check the input fields.");
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
        }
    }

    [System.Serializable]
    private class AuthenticationResponse
    {
        public string token;
    }
}
