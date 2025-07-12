using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void StartGame()
    {
        _gameController.StartGame();
    }

    public void StartCountdown()
    {
        _animator.SetTrigger("StartCountdown");
    }
}