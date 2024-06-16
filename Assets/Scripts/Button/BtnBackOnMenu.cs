using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnBackOnMenu : BaseButon
{
    protected override void OnClick()
    {
        SceneManager.LoadScene("Intro");
    }
}
    
