using UnityEngine;

public class PutThingsDown : MonoBehaviour
{
    [SerializeField] private GameObject _things;
    [SerializeField] private GameObject _thingsDammy;
    private GameManager _gameManager = GameManager.Instance;
    private Camera _mainCam;
    private float _thingsPrice;
    private bool _canPutThings = false;
    public static PutThingsDown Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _mainCam = Camera.main;
    }
    private void Update()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (_gameManager._putState != GameManager.PutState.None && _gameManager._putState != GameManager.PutState.Delete)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //ゲームオブジェクトとマウスポインターの重なった座標を整数にしてｙに１足してゲームオブジェクトの上にくるようにする。
                Vector3 pointerPosInt = new(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y) + 1, Mathf.FloorToInt(hit.point.z));
                _thingsDammy.transform.position = pointerPosInt;
                _thingsDammy.transform.position += new Vector3(0, pointerPosInt.y - _thingsDammy.GetComponent<ThingsDammy>()._BottomPos.transform.position.y, 0);

                if (hit.collider.gameObject.CompareTag("Ground") && GridManager.Instance.CanPlaceObjectAtPosition(pointerPosInt))
                {
                    _thingsDammy.SetActive(true);
                    _canPutThings = true;

                    if(Input.GetKeyDown(KeyCode.R))
                    {
                        _thingsDammy.transform.Rotate(0, 90, 0);
                        if(_thingsDammy.transform.rotation.y >= 360)
                        {
                            _thingsDammy.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                }
                else
                {
                    _thingsDammy.SetActive(false);
                    _canPutThings = false;
                }
            }
        }
        else
        {
            _canPutThings = false;
        }
        OnClickPutThings();
    }

    private void OnClickPutThings()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canPutThings && _gameManager._putState != GameManager.PutState.None)
        {
            if(_gameManager.GetMoney() >= _thingsPrice)
            {
                Things things = Instantiate(_things, _thingsDammy.transform.position, _thingsDammy.transform.rotation).GetComponent<Things>();
                GridManager.Instance.RegisterPlacedObject(_thingsDammy.transform.position, things.gameObject);
                things.FindNextThings();
                _gameManager.RemoveMoney(_thingsPrice);
            }
        }
    }

    public void ChangeThings(GameObject things, GameObject thingsDammy, float thingsPrice)
    {
        if (_things != null) { _thingsDammy.SetActive(false); }
        _things = things;
        _thingsDammy = thingsDammy;
        _thingsPrice = thingsPrice;
        Debug.Log($"Put Things Changed: {_things.name}, {_thingsDammy.name}");
    }
}
