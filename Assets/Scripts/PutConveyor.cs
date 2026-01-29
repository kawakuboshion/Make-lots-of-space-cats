using UnityEngine;
using UnityEngine.InputSystem;

public class PutConveyor : MonoBehaviour
{
    [SerializeField] private GameObject _Conveyor;
    [SerializeField] private GameObject _ConveyorDammy;
    private Collider[] _overlapBuffer = new Collider[8];
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }
    private void Update()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //ゲームオブジェクトとマウスポインターの重なった座標を整数にしてｙに１足してゲームオブジェクトの上にくるようにする。
            Vector3 pointerPosInt = new (Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.y)　+ 1, Mathf.FloorToInt(hit.point.z));
            _ConveyorDammy.transform.position = pointerPosInt;
        }

        OnClickConveyor();
    }

    private void OnClickConveyor()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var dammyCollitions = Physics.OverlapBox(_ConveyorDammy.transform.position, new Vector3(0.5f, 0.5f, 0.5f));
            foreach (var collition in dammyCollitions)
            {
                if (collition.gameObject.GetComponent<Conveyor>() != null)
                {
                    Destroy(collition.gameObject);
                    return;
                }
            }
            Instantiate(_Conveyor, _ConveyorDammy.transform.position, Quaternion.identity);
        }
    }
}
