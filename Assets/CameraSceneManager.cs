using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class CameraSceneManager : MonoBehaviour
    {
        public void GoToScene(Scenes scene)
        {
            switch (scene)
            {

                case Scenes.Forest1:
                    gameObject.transform.position = new Vector3(-1.33f, 0.7f, -17.58f);
                    break;
                case Scenes.Meadow:
                    gameObject.transform.position = new Vector3(-50f, 0.7f, -17.58f);
                    break;
                case Scenes.Forest2:
                    gameObject.transform.position = new Vector3(-151.56f, 0.7f, -17.58f);
                    break;
                case Scenes.House:
                    gameObject.transform.position = new Vector3(-200f, 0.7f, -17.58f);
                    break;
                case Scenes.Shore:
                    gameObject.transform.position = new Vector3(-101.73f, 0.7f, -17.58f);
                    break;
                case Scenes.Pond:
                    gameObject.transform.position = new Vector3(-1.33f, 0.7f, -17.58f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scene), scene, null);
            }
        }
    }
}