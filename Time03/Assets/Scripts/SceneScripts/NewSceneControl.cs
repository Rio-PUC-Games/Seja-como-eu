﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneControl : MonoBehaviour
{

    void Awake()
    {
        LoadScene();
    }

    public static string CurrentScene;

    public static void LoadScene() {
        SceneManager.LoadSceneAsync(CurrentScene,LoadSceneMode.Single);
    }
}
