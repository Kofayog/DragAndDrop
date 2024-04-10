using UnityEngine;

public interface IPlaceable
{
    bool IsEmpty { get; }
    
    void Preview(Transform target);
    void Place(GameObject target);
}
