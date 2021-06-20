using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionByTimer : MonoBehaviour
{
    public string ToSceneName;
    public int WaitForInSeconds;

    private DateTimeOffset startTime;
    void Start()
    {
        startTime = DateTimeOffset.UtcNow;
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTimeOffset.UtcNow - startTime > TimeSpan.FromSeconds(WaitForInSeconds))
            SceneManager.LoadScene(ToSceneName);
    }
}
