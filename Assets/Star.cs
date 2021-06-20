using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            if (MouseHelper.IsMouseAboveObjectWithTag("Star"))
            {
                Debug.Log("It is a star");
                SceneManager.LoadScene("Final");
            }
        }
    }
}
