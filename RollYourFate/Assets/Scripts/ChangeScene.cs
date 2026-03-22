using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void Change(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);

    }
}
