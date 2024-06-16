using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnchoseLevel : BaseButon
{
    
    protected override void OnClick()
    {   
        
        SceneManager.LoadScene(button.gameObject.name);
    }
}
