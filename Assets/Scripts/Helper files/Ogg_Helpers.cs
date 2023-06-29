using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ogg_Helpers
{

    public static void Abort(string msg = null)
    {
        if (msg != null)
            Debug.LogError(msg);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}