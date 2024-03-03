using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sssssss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetResolution();
    }

    public void SetResolution()
    {
        int width = 900;
        int height = 1900;

        Screen.SetResolution(width, height, true);
    }
}
