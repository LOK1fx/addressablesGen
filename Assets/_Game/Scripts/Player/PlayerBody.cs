using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private Transform _directionTransform;
    [SerializeField] private float _rotationSpeed = 6f;
    
    private void LateUpdate()
    {
        if(_directionTransform == null)
            return;
        
        transform.rotation = Quaternion.Lerp(transform.rotation, _directionTransform.rotation,
            _rotationSpeed * Time.deltaTime);
    }
}
