using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnPlay : BaseButon
{
    protected override void OnClick()
    {
        SceneManager.LoadScene("ChoseLevel");
    }
   
    
}
