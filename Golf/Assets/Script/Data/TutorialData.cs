﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialData
{
    public enum TUTORIAL_TYPE
    {
        MAIN,
        POSE,
        ANGLE,
        TEMPO,
    }

    public string Title;
    public string Url;

    public TutorialData(string title, string url)
    {
        Title = title;
        Url = url;
    }
}