using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeScene : MonoBehaviour
{


    public void Change(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);

    }
}
