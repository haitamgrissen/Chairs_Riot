using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    [SerializeField]
    Transform target;

    public float FollowTime = 5;
    public float ZOffsetDistance = 7;
    public float YOffsetDistance = 15;

    public float ZLookOffset = 2;
    public float YLookOffset = 1;
    Vector3 desiredPosition = Vector3.zero;
    Vector3 desiredLookPosition = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        desiredPosition = target.position - (Vector3.forward * ZOffsetDistance) + (Vector3.up * YOffsetDistance);
        transform.position = desiredPosition;

        desiredLookPosition = target.position + (Vector3.forward * ZLookOffset) + (Vector3.up * YLookOffset);
        transform.LookAt(desiredLookPosition);
    }

    private void Update()
    {
        getDesiredposition();
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, FollowTime * Time.deltaTime);
        
        //transform.LookAt(desiredLookPosition);
    }

    void getDesiredposition()
    {
        desiredPosition = target.position - (Vector3.forward * ZOffsetDistance) + (Vector3.up * YOffsetDistance);
        desiredPosition.x = 0;
        desiredLookPosition = target.position + (Vector3.forward * ZLookOffset) + (Vector3.up * YLookOffset);
    }
}