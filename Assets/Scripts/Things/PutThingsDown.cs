using UnityEngine;

public class PutThingsDown : MonoBehaviour
{
    [SerializeField] private GameObject _things;
    [SerializeField] private GameObject _thingsDammy;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameManager _gameManager = GameManager.Instance;
    private ChangePutThings.PutState _PutState;
    private Camera _mainCam;
    private float _thingsPrice;
    private bool _canPutThings = false;

    private void Start()
    {
        _mainCam = Camera.main;
        if(_gameManager == null)
        {
            _gameManager = GameManager.Instance;
        }
    }
    private void Update()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (_PutState != ChangePutThings.PutState.None && _PutState != ChangePutThings.PutState.Delete)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //ゲームオブジェクトとマウスポインターの重なった座標を整数にしてｙに１足してゲームオブジェクトの上にくるようにする。
                Vector3 pointerPosInt = new(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y) + 1, Mathf.FloorToInt(hit.point.z));
                _thingsDammy.transform.position = pointerPosInt;
                _thingsDammy.transform.position += new Vector3(0, pointerPosInt.y - _thingsDammy.GetComponent<ThingsDammy>()._BottomPos.transform.position.y, 0);

                if (hit.collider.gameObject.CompareTag("Ground") && _gridManager.CanPlaceObjectAtPosition(pointerPosInt))
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canPutThings && _PutState != ChangePutThings.PutState.None)
        {
            if(_gameManager.GetMoney() >= _thingsPrice)
            {
                Things things = Instantiate(_things, _thingsDammy.transform.position, _thingsDammy.transform.rotation).GetComponent<Things>();
                _gameManager.SetLogText($"{things._thingName}を置いた");
                _gridManager.RegisterPlacedObject(_thingsDammy.transform.position, things.gameObject);
                _gameManager.RemoveMoney(_thingsPrice);
            }
            else
            {
                _gameManager.SetLogText($"お金が足りない！{_thingsPrice}円必要です", true);
            }
        }
    }

    public void ChangeThings(GameObject things, GameObject thingsDammy, float thingsPrice)
    {
        HideDammy();
        _things = things;
        _thingsDammy = thingsDammy;
        _thingsPrice = thingsPrice;
        Debug.Log($"Put Things Changed: {_things.name}, {_thingsDammy.name}");
    }

    public void ChangePutState(ChangePutThings.PutState putState)
    {
        _PutState = putState;
        HideDammy();
    }

    public void HideDammy()
    {
        if (_thingsDammy != null)
        {
            _thingsDammy.SetActive(false);
        }
    }
}
