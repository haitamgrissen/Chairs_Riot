using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Character_Controller : MonoBehaviour
{
    [Space(10)]
    [Header("Mouvement settings")]
    public float Speed;
    public float Acceleration;
    [Space(10)]
    [Header("Rotation settings")]
    public float RotationSharpness;
    public bool InvertRotation = true;
    [Space(10)]
    [Header("Joystick settings")]
    public Joystick joystick;



    Vector3 input;
    Vector3 velocity, desiredVelocity;
    Quaternion rotation; 
    Rigidbody rb;
    Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        velocity = Vector3.zero;
        input = Vector3.zero;
        rotation = Quaternion.LookRotation(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        input.x = joystick.Horizontal;
        input.z = joystick.Vertical;
        input = Vector3.ClampMagnitude(input, 1f);
        desiredVelocity = input * Speed;
        desiredVelocity = cam.transform.TransformDirection(desiredVelocity);
        desiredVelocity.y = 0;
    }

    private void FixedUpdate()
    {
        //velocity.x = input.x;
        //velocity.z = input.y;
        //velocity *= Time.deltaTime
        AdjustVelocity();
        AdjustRotation();
        rb.velocity = velocity;
        rb.rotation = rotation;
    }

    void AdjustVelocity()
    {
        velocity = rb.velocity;
        float currentx = Vector3.Dot(velocity, Vector3.right);
        float currentz = Vector3.Dot(velocity, Vector3.forward);
        float maxspeedchange = Acceleration * Time.deltaTime;
        float newx = Mathf.MoveTowards(currentx, desiredVelocity.x, maxspeedchange);
        float newz = Mathf.MoveTowards(currentz, desiredVelocity.z, maxspeedchange);
        velocity += (Vector3.right * (newx - currentx)) + (Vector3.forward * (newz - currentz));
    }
    
    void AdjustRotation()
    {
        if (input.sqrMagnitude == 0)
            return;
        desiredVelocity.x += 0.00001f;
        desiredVelocity *= InvertRotation ? -1 : 1;
        Quaternion desiredrotation = Quaternion.LookRotation (desiredVelocity);
        rotation = Quaternion.Slerp(transform.rotation, desiredrotation, RotationSharpness * Time.deltaTime);
    }
}
