using UnityEngine;
using UnityEngine.Events;

public class GoalController : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            onTriggerEnter.Invoke();
        }
    }
}