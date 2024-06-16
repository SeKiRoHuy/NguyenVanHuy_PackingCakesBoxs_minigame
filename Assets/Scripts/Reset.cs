using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    void Awake()
    {
        // Đặt lại Time.timeScale về giá trị mặc định khi scene được tải
        Time.timeScale = 1f;
    }


}
