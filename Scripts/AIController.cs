using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AIController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _offset = 0.2f;
    
    private Rigidbody2D _rigidbody;
    private Transform _ball;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ball = GameObject.FindGameObjectWithTag("Ball").transform;
    }
    private void Update()
    {
        MoveComputer();
    }

    private void MoveComputer()
    {
        if (_ball.position.y > transform.position.y + _offset)
        {
            _rigidbody.linearVelocity = Vector2.up * _speed;
        } else if (_ball.position.y < transform.position.y - _offset)
        {
            _rigidbody.linearVelocity = Vector2.down * _speed;
        }
        else
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}
