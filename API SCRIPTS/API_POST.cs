using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using SimpleJSON; // Import SimpleJSON

public class DataSender : MonoBehaviour
{
    public TMP_InputField firstNameInput;
    public TMP_InputField lastNameInput;
    public TMP_InputField nicInput;
    public TMP_InputField usernameInput;
    public TMP_InputField phoneNumberInput;
    public TMP_InputField emailInput;
    public TMP_InputField memberStatusInput; // Change memberStatusToggle to memberStatusInput

    public void SendData()
    {
        StartCoroutine(UploadData());
    }

    IEnumerator UploadData()
    {
        // Create a JSON object representing the data
        JSONObject data = new JSONObject();

        // Add fields to the JSON object
        data["firstname"] = firstNameInput.text;
        data["lastname"] = lastNameInput.text;
        data["nic"] = nicInput.text;
        data["username"] = usernameInput.text;
        data["phoneNumber"] = phoneNumberInput.text;
        data["email"] = emailInput.text;
        data["demand_response_program_member_status"] = memberStatusInput.text; // Use memberStatusInput.text to get the input

        // Convert the JSON object to a string
        string jsonData = data.ToString();

        Debug.Log(jsonData);

        // Create a UnityWebRequest with POST method and set the JSON data as the body
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm("http://localhost:3000/files", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for response
            yield return www.SendWebRequest();

            // Check for errors
            if (www.result != UnityWebRequest.Result.Success)
            {
                
                Debug.LogError(www.error);
                
            }
            else
            {
                Debug.Log("Data sent successfully!");
            }
        }
    }
}
