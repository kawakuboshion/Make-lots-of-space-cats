using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    [SerializeField] private Canvas _infoCanvas;
    [SerializeField] private Image _spaceImage;
    [SerializeField] private Slider _infoProcessSlider;
    [SerializeField] public float _InfoProcessLevel = 100f;
    [SerializeField] private float _initializedInfoProcessLevel = 100f;
    [SerializeField] private float _speed = 1.0f;
    public Things _inThings;
    public bool _isStopped = false;
    private Vector3 _startPos;
    private Vector3 _destination;
    private float _moveTime;

    public void Initialize()
    {
        _InfoProcessLevel = _initializedInfoProcessLevel;
        _infoProcessSlider.maxValue = _InfoProcessLevel;
        _infoProcessSlider.value = _InfoProcessLevel;
        _spaceImage.color = new Color(_spaceImage.color.r, _spaceImage.color.g, _spaceImage.color.b, 0f);//‰F’ˆ‚Ì‰æ‘œ‚ð“§–¾‚É‚·‚é
    }

    public void SetDestination(Vector3 destination)
    {
        _isStopped = false;
        _startPos = transform.position;
        _destination = destination;
    }

    public void SetInThings(Things things)
    {
        _inThings = things;
    }

    public void StartMoving()
    {
        _isStopped = false;
        _moveTime = 0.0f;
    }

    public void ReduceInfoProcessLevel(float amount)
    {
        _InfoProcessLevel -= amount;
        if (_InfoProcessLevel < 0f)
        {
            _InfoProcessLevel = 0f;
        }
        //‰F’ˆ‚Ì‰æ‘œ‚ðî•ñˆ—“x‚ªŒ¸‚é‚É‚Â‚ê‚Ä™X‚ÉŒ©‚¦‚é‚æ‚¤‚É‚·‚é
        float alpha = Mathf.InverseLerp(_initializedInfoProcessLevel, 0, _InfoProcessLevel);
        _spaceImage.color = new Color(_spaceImage.color.r, _spaceImage.color.g, _spaceImage.color.b, alpha);
        _infoProcessSlider.value = _InfoProcessLevel;
    }
    // Update is called once per frame
    void Update()
    {
        _infoCanvas.transform.LookAt(Camera.main.transform);
        if (_isStopped) { return; }

        _moveTime += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPos, _destination, _moveTime);

        if(_moveTime >= 1.0f)
        {
            if(_inThings != null)
            {
                _inThings.MoveTheCat();
            }
        }
    }
}
