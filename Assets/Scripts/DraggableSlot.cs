using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform _root;
    [SerializeField] private GameObject _slotObject;
    
    private const float CURSOR_DISTANCE = 4f;
    private const float RAYCAST_DISTANCE = 100f;
    
    private readonly RaycastHit[] _raycastHits = new RaycastHit[5];
    private int _hitAmount;
    
    private bool _isDragging;
    private GameObject _selectedObject;
    private IPlaceable _placeable;

    private Camera _camera;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _selectedObject = _root.GetChild(0).gameObject;
        _selectedObject.transform.SetParent(null);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ReturnToSlot();

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        _hitAmount = Physics.RaycastNonAlloc(ray, _raycastHits, RAYCAST_DISTANCE, LayerTags.PlaceableLayer);

        if (_hitAmount > 0 && _raycastHits[0].transform.TryGetComponent(out _placeable))
        {
            _placeable.Place(_selectedObject);
        }
       
        _selectedObject = null;
        _isDragging = false;
    }

    private void OnDragObject()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        _hitAmount = Physics.RaycastNonAlloc(ray, _raycastHits, RAYCAST_DISTANCE);

        if (_hitAmount == 0)
        {
            _selectedObject.transform.position = ray.GetPoint(CURSOR_DISTANCE);
            _selectedObject.transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
            return;
        }

        for (int i = 0; i < _hitAmount; i++)
        {
            var target = _raycastHits[i].transform;

            if (target == transform)
            {
                ReturnToSlot();
                continue;
            }

            _selectedObject.transform.position = _raycastHits[i].point;
            _selectedObject.transform.rotation = Quaternion.LookRotation(_camera.transform.forward);

            if (!target.TryGetComponent(out _placeable))
            {
                continue;
            }

            _placeable.Preview(_selectedObject.transform);

            break;
        }
    }

    private void ReturnToSlot()
    {
        _selectedObject.transform.SetParent(_root);
        _selectedObject.transform.localPosition = Vector3.zero;
        _selectedObject.transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (!_isDragging)
        {
            return;
        }

        OnDragObject();
    }
    
    private void Awake()
    {
        _camera = Camera.main;
        Instantiate(_slotObject, _root);
    }
}
