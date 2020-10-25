using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FecharJogo : MonoBehaviour
{
    void Start()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }


}
