using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehavior : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public float JumpVelocity = 1.0f;
    public float MaxVelocity = 1.0f;
    public float SpeedLoss = .1f;

    private Rigidbody2D _rigidBody;
    private Vector2 _velocity = new Vector2();


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _velocity.y = 0.0f;
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetButton("Jump");

        _velocity += new Vector2(horizontal, 0.0f);
        _velocity.x = Mathf.Lerp(_velocity.x, 0, SpeedLoss);

        if (jump)
        {
            Debug.Log("jumping");
            _velocity.y += JumpVelocity;
            jump = false;
        }

        _velocity.x = Mathf.Clamp(_velocity.x, -MaxVelocity, MaxVelocity);
        _velocity.y = Mathf.Clamp(_velocity.y, -MaxVelocity, MaxVelocity);

        transform.Translate(_velocity * MovementSpeed);
    }
}
