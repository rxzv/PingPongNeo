using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _minDirection = 0.5f;
    [SerializeField] private GameObject _VFX;
    [SerializeField] private GameObject _ping;
    [SerializeField] private GameObject _pong;
    [SerializeField] private AudioClip[] _sounds;
        
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private AudioSource _audio;
    private Data _data;
    
    private bool _stopped = false;
    private bool _pongCountDown = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _data = Data.Instance;
        
    }

    private void FixedUpdate()
    {
        if(_stopped) return;
        _rigidbody.MovePosition(_rigidbody.position + _direction * _speed * Time.fixedDeltaTime);
    }

    private void PingPongVisible()
    {
        if (_pongCountDown)
        {
            var pong = Instantiate(_pong, _pong.transform.position, Quaternion.identity);
            Destroy(pong, 0.05f);
            _pongCountDown = false;
        }
        else
        {
            var ping = Instantiate(_ping, _ping.transform.position, Quaternion.identity);
            Destroy(ping, 0.05f);
            _pongCountDown = true;
        }
    }

    public void Stop()
    {
        _stopped = true;
    }

    public void Go()
    {
        ChooseDirection();
        _stopped = false;
    }

    public void GoAbortPaused()
    {
        _stopped = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool hit = false;
        if (other.CompareTag("Wall"))
        {
            PingPongVisible();
            _direction.y = -_direction.y;
            hit = true;
        }

        if (other.CompareTag("Racket"))
        {
            PingPongVisible();
            Vector2 newDirection = (transform.position - other.transform.position).normalized;
            
            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), _minDirection);
            newDirection.y = Mathf.Sign(newDirection.y) * Mathf.Max(Mathf.Abs(newDirection.y), _minDirection);
            
            _direction = newDirection;
            hit = true;
        }

        if (hit)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            if (_data.VFX == true)
            {
                var vfx = Instantiate(_VFX, transform.position, transform.rotation);
                Destroy(vfx, 3f);
            }

            if (_data.sound == true)
            {
                _audio.PlayOneShot(_sounds[Random.Range(0, _sounds.Length)]);
            }
        }
    }

    private void ChooseDirection()
    {
        float signX = Mathf.Sign(Random.Range(-1f, 1f));
        float signY = Mathf.Sign(Random.Range(-1f, 1f));
        
        _direction = new Vector2(_minDirection * signX, _minDirection * signY);
    }
}
