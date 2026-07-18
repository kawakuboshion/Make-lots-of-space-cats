using UnityEngine;

public class OnClickNextImage : MonoBehaviour
{
    [SerializeField] private HowToPlayManager _manager;

    public void OnClick()
    {
        AudioManager.Instance.PlaySE(AudioManager.SE.OnClick);
        _manager.NextImage();
    }
}
