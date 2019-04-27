using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour, IProjectileBehavior
{
    public float ShotVelocity = 1.0f;
    public float SecondsToLive = 5;

    private float _instantiated_time; 
    // Start is called before the first frame update
    void Start()
    {
        _instantiated_time = Time.fixedTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > SecondsToLive + _instantiated_time)
        {
            Destroy(this.gameObject);
        }

        Vector3 direction = new Vector3(1, 0);
        var target = ShotVelocity * direction;

        transform.Translate(target * Time.deltaTime);
        
    }

    void FixedUpdate()
    {
    }
}
