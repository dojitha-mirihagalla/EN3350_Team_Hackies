using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;

public class RestApi : MonoBehaviour
{
    private string url = "http://localhost:3000/files";

    public TMP_Text FirstName;
    public TMP_Text LastName;
    public TMP_Text NIC;
    public TMP_Text Username;
    public TMP_Text MobileNumber;
    public TMP_Text EmailAddress;
    public TMP_Text DemandResponseProgramMemberStatus;

    public TMP_InputField indexInput;

    private int index = 0;

    void Start()
    {
        // Display loading texts initially
        SetLoadingTexts();
        // Trigger GetData when the index changes via the input field
        indexInput.onValueChanged.AddListener(delegate { IndexValueChanged(); });
        StartCoroutine(GetData());
    }

    // Read the input index from the user
    public void IndexValueChanged()
    {
        if (int.TryParse(indexInput.text, out int parsedIndex))
        {
            index = parsedIndex;
            Debug.Log("Index changed: " + index);
            // Fetch data with new index
            StartCoroutine(GetData());
        }
        else
        {
            Debug.LogError("Invalid index input: " + indexInput.text);
        }
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error fetching data: " + webRequest.error);
            }
            else
            {
                string response = webRequest.downloadHandler.text;
                JSONNode data = JSON.Parse(response);

                if (index >= 0 && index < data.Count)
                {
                    // Display fetched data
                    FirstName.text = "First Name: " + data[index]["firstname"];
                    LastName.text = "Last Name: " + data[index]["lastname"];
                    NIC.text = "NIC: " + data[index]["nic"];
                    Username.text = "Username: " + data[index]["username"];
                    MobileNumber.text = "Mobile Number: " + data[index]["phoneNumber"];
                    EmailAddress.text = "Email Address: " + data[index]["email"];
                    DemandResponseProgramMemberStatus.text = "Demand Response Program Member Status: " + data[index]["demand_response_program_member_status"];
                }
                else
                {
                    Debug.LogError("Index out of range: " + index);
                }
            }
        }
    }

    // Set loading texts for all fields
    void SetLoadingTexts()
    {
        FirstName.text = "First Name: Loading...";
        LastName.text = "Last Name: Loading...";
        NIC.text = "NIC: Loading...";
        Username.text = "Username: Loading...";
        MobileNumber.text = "Mobile Number: Loading...";
        EmailAddress.text = "Email Address: Loading...";
        DemandResponseProgramMemberStatus.text = "Demand Response Program Member Status: Loading...";
    }
}
