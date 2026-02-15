using UnityEngine;

public class OnClickLoadScene : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        AudioManager.Instance.PlaySE(AudioManager.SE.OnClick);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
