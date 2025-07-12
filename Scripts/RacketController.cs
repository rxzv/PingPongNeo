using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RacketController : MonoBehaviour
{
    [SerializeField] private int _speed;
    
    [SerializeField] private KeyCode _upKey = KeyCode.W;
    [SerializeField] private KeyCode _downKey = KeyCode.S;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveRacket();
    }

    private void MoveRacket()
    {
        bool up = Input.GetKey(_upKey);
        bool down = Input.GetKey(_downKey);
        
        if (up)
        {
            _rb.linearVelocity = Vector2.up * _speed;
        }

        if (down)
        {
            _rb.linearVelocity = Vector2.down * _speed;
        }

        if (!up && !down)
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
