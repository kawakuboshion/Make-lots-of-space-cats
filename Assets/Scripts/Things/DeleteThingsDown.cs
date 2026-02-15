using UnityEngine;

public class DeleteThingsDown : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameObject _deleteDammy;
    private ChangePutThings.PutState _putState;
    private Camera _mainCam;
    private bool _canDeleteThings = false;
    private GameManager _gameManager = GameManager.Instance;
    private void Start()
    {
        _mainCam = Camera.main;
        if (_gameManager == null)
        {
            _gameManager = GameManager.Instance;
        }
    }
    private void Update()
    {
        if (_putState != ChangePutThings.PutState.Delete) { return; }

        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //ゲームオブジェクトとマウスポインターの重なった座標を整数にしてｙに１足してゲームオブジェクトの上にくるようにする。
            Vector3 pointerPosInt = new(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y) + 1, Mathf.FloorToInt(hit.point.z));
            _deleteDammy.transform.position = pointerPosInt;
            if (!_gridManager.CanPlaceObjectAtPosition(pointerPosInt))
            {
                _deleteDammy.SetActive(true);
                _canDeleteThings = true;
            }
            else
            {
                _deleteDammy.SetActive(false);
                _canDeleteThings = false;
            }
        }
        OnClickDeleteThings();
    }

    private void OnClickDeleteThings()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canDeleteThings)
        {
            GameObject remove = _gridManager.GetObjectAtPosition(_deleteDammy.transform.position);
            _gridManager.UnregisterPlacedObject(_deleteDammy.transform.position);
            Things things = remove.GetComponent<Things>();
            _gameManager.SetLogText($"{things._thingName}を消した", true);
            _gameManager.AddMoney(things._Price);
            if(things._cat != null)
            {
                things._cat.GetComponent<PooledObject>().Release();
            }
            Destroy(remove);
        }
    }

    public void ChangePutState(ChangePutThings.PutState putState)
    {
        _putState = putState;
    }
}
