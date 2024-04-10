using UnityEngine;

public class Cell : MonoBehaviour, IPlaceable
{
    [SerializeField] private Transform _root;
    [SerializeField] private Transform _transform;
    [SerializeField] private Renderer _renderer;

    public bool IsEmpty => _objectInSlot == null;
    public Bounds Bounds => _renderer.bounds;
    public Transform Transform => _transform;
    
    private GameObject _objectInSlot;

    public void Preview(Transform target)
    {
        if (!IsEmpty)
        {
            return;
        }
        
        target.SetParent(_root);
        target.localPosition = Vector3.zero;
        target.localRotation = Quaternion.identity;
    }

    public void Place(GameObject targetObject)
    {
        if (!IsEmpty)
        {
            return;
        }

        _objectInSlot = Instantiate(targetObject, _root);
    }
}
