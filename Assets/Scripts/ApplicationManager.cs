﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 144;
        //Time.timeScale = 0;
    }
}
