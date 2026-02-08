using UnityEngine;

public class OnClickLoadScene : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
