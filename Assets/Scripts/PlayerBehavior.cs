using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehavior : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public float JumpVelocity = 1.0f;
    public float MaxVelocity = 1.0f;
    public float SpeedLoss = .1f;
    public BasicBullet Bullet;
    public int FireRateInMilliseconds = 100;

    private Rigidbody2D _rigidBody;
    private Vector2 _velocity = new Vector2();
    private float _lastFired;
    private Camera _mainCamera;



    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        _lastFired = Time.fixedTime;
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
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
        bool fire = Input.GetButton("Fire1");
        if (fire)
        {
            Fire();
        }

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

    private void Fire()
    {
        if (Time.fixedTime < _lastFired + FireRateInMilliseconds / 1000f)
        {
            return;
        }

        _lastFired = Time.fixedTime;

        Vector3 mousePoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("player");
        Debug.Log(transform.position);
        Debug.Log("mouse");
        Debug.Log(mousePoint);
        mousePoint.z = 0.0f;
        Vector3 direction = mousePoint - transform.position;
        direction = direction.normalized;
        
        Instantiate(Bullet, transform.position, Quaternion.identity);
        Bullet.Direction = direction;
    }
}
