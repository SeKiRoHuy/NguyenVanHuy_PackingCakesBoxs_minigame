using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnOK : BaseButon
{
    protected override void OnClick()
    {
        SceneManager.LoadScene("Level1");
    }
}
