using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerSeekArrive : MonoBehaviour
{

    /**

data kinematic : posisi dan orientasi pada karakter telah terdapat pada class transform
untuk mengakses gunakan transform
        **/


    //Transform charakter; // sudah tidak perlu karena sudah bisa diakses dengan syntax this.transform 
    public Transform _target;
    public float _maxSpeed;
    public int _maxAcceleration;
    public int _maxBrakeForce;
    public Vector3 _velocity = Vector3.zero;
    public Vector3 _targetVelocity ;
    public float _targetSpeed;
    // public Vector3 _accel;


    public float _targetRadius;  //Holds the radius for arriving at the target
    public float _slowRadius;  //Holds the radius for beginning to slow down
    
    public float distance;

    void Start()
    {
        _velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        _velocity = _velocity + getSteering()._linear * Time.deltaTime; // Vt = Vo +a.t

        if (distance < 0.01 && _targetSpeed < 0.01)
        {
            _velocity = Vector3.zero;
        }

        if (_velocity.magnitude > _maxSpeed)
        {
            _velocity = _velocity.normalized * _maxSpeed;

        }

        this.transform.position = transform.position + _velocity * Time.deltaTime;
        this.transform.eulerAngles = SteeringData.getNewOrientation(transform.eulerAngles, _velocity);

    }

    public SteeringData getSteering()
    {
        SteeringData _SteeringOut = new SteeringData();
        _SteeringOut._linear = _target.position - transform.position; //#direction

        distance = _SteeringOut._linear.magnitude;
       
        if (distance > _slowRadius)
        {
            
            _targetSpeed = _maxSpeed;
        }

        else if (distance <= _targetRadius)
        {
            _targetSpeed = 0;
        }

        else
        {
            _targetSpeed = _maxSpeed*distance /_slowRadius;
        }

          _targetVelocity = _SteeringOut._linear.normalized * _targetSpeed;
          _SteeringOut._linear = (_targetVelocity - _velocity);

       // if (_targetSpeed < _maxSpeed)  //jika melambat gunakan brakeForce
       // {
       //         _SteeringOut._linear = _SteeringOut._linear.normalized; // normalize membuat resultan vektor = 1.
       //         _SteeringOut._linear *= _maxBrakeForce;   
       // }
      //  else
       // {
            if (_SteeringOut._linear.magnitude > _maxAcceleration)
            {
                _SteeringOut._linear = _SteeringOut._linear.normalized; // normalize membuat resultan vektor = 1.
                _SteeringOut._linear *= _maxAcceleration;
            }
      //  }
      
        return _SteeringOut;
    }

    


}
