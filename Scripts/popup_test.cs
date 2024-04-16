using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Popup ;

public class popup : MonoBehaviour
{
    [TextArea (5, 20)]public string longText ;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Button1 () {
      Popup.Show ("Success", "Your account updated successfully.", "OK", PopupColor.Green) ;
   }
    // Update is called once per frame
    void Update()
    {
        
    }
}
