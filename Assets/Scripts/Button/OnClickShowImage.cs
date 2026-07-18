using UnityEngine;

public class OnClickShowImage : MonoBehaviour
{
    [SerializeField] private HowToPlayManager _manager;

    public void OnClick()
    {
        AudioManager.Instance.PlaySE(AudioManager.SE.OnClick);
        _manager.ShowImage();
    }
}
