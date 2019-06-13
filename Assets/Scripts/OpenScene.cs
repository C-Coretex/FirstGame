using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScene : MonoBehaviour
{
    public int index;
    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
