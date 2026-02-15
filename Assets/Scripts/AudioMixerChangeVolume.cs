using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerChangeVolume : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private VolumeType _volumeType = VolumeType.Master;
    private AudioManager _audioManager;

    public enum VolumeType
    {
        Master,
        BGM,
        SE
    }

    private void Start()
    {
        if(_audioManager == null)
        {
            _audioManager = AudioManager.Instance;
        }
        switch(_volumeType)
        {
            case VolumeType.Master:
                _volumeSlider.value = _audioManager.ConvertDB2Volume(_audioManager.GetMasterVolume());
                break;
            case VolumeType.BGM:
                _volumeSlider.value = _audioManager.ConvertDB2Volume(_audioManager.GetBGMVolume());
                break;
            case VolumeType.SE:
                _volumeSlider.value = _audioManager.ConvertDB2Volume(_audioManager.GetSEVolume());
                break;
        }
    }

    public void OnVolumeSliderChanged(float value)
    {
        switch(_volumeType)
        {
            case VolumeType.Master:
                _audioManager.SetMasterVolume(value);
                break;
            case VolumeType.BGM:
                _audioManager.SetBGMVolume(value);
                break;
            case VolumeType.SE:
                _audioManager.SetSEVolume(value);
                break;
        }
        _audioManager.PlaySE(AudioManager.SE.Change_AudioValue);
    }
}
