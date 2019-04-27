using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehavior : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public float JumpHeight = 1.0f;
    public float MaxVelocity = 1.0f;

    private Rigidbody2D _rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var velocity = new Vector3();
        velocity.x = Input.GetAxis("Horizontal");
        velocity = velocity.normalized;

        transform.Translate(velocity * MovementSpeed);
    }
}
