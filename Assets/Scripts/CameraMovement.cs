using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 1f;

    public Transform target;
    public void SetTarget(Transform target)
    {
        this.target = target;
        oldTargetPosition = target.position;
        transform.position = target.position + offset;
    }


    private float _verticalRotation;
    private float _horizontalRotation;
    public Vector3 offset = new Vector3(0, 1.7f, -1.3f);
    private Vector3 oldTargetPosition= new Vector3(2.5f, 0, -3.6f);
    
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }  
    
    
    
    
    private void LateUpdate()
    {
        if (target == null) return;

        var deltaPosition = target.position - oldTargetPosition;
        transform.Translate(deltaPosition, Space.World);
        
        
        
        float angleChange = Input.GetAxis("Mouse X") * _mouseSensitivity;
        transform.RotateAround(target.transform.position, Vector3.up, angleChange);
        transform.LookAt(target.transform.position+Vector3.up*1.5f);
        
        
        if(deltaPosition.magnitude>0)oldTargetPosition = target.position;
        
    }
}