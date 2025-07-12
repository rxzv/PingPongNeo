using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _ball, _racketOne, _racketTwo;
    [SerializeField] private Starter _starter;
    [SerializeField] private Text _winnerText;
    
    [SerializeField] private TextMeshProUGUI _scoreTextLeft, _scoreTextRight;
    
    [SerializeField] private Canvas _gameOverCanvas, _gameCanvas, _menuCanvas;
    
    private BallController _ballController;
    private Vector3 _startingBallPosition;
    private Camera _camera;
    private Data _data;
    
    private int _scoreLeft = 0, _scoreRight = 0, _esc = 0;
    private bool _started = false, _gameOver = false, _gameStopped = false;

    private void Start()
    {
        _ballController = _ball.GetComponent<BallController>();
        _startingBallPosition = _ball.transform.position;
        _camera = Camera.main;
        _data = Data.Instance;
        if (_data.music == false)
        {
            _camera.GetComponent<AudioSource>().Stop();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
            GamePausedChecker();
        GameOver();
        if(_started)
            return;
        _started = true;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
           StartGame();
           return;
        }
        _starter.StartCountdown();
    }

    private void GamePausedChecker()
    {
        if (_gameStopped)
        {
            if (_esc == 0)
            {
                _ballController.Go();
                _esc = 1;
            }
            else
            {
                _ballController.GoAbortPaused();
            }
            _gameStopped = false;
            _menuCanvas.gameObject.SetActive(false);
            _gameCanvas.gameObject.SetActive(true);
            _racketOne.GetComponent<RacketController>().enabled = true;
            if (_racketTwo.GetComponent<RacketController>())
            {
                _racketTwo.GetComponent<RacketController>().enabled = true;
            }
            else
            {
                _racketTwo.GetComponent<AIController>().enabled = true;
            }
        }
        else
        {
            _gameStopped = true;
            _ballController.Stop();
            _racketOne.GetComponent<RacketController>().enabled = false;
            if (_racketTwo.GetComponent<RacketController>())
            {
                _racketTwo.GetComponent<RacketController>().enabled = false;
            }
            else
            {
                _racketTwo.GetComponent<AIController>().enabled = false;
            }
            _gameCanvas.gameObject.SetActive(false);
            _menuCanvas.gameObject.SetActive(true);
        }
        
    }
    private void GameOver()
    {
        if (_scoreLeft >= 5 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            _gameCanvas.gameObject.SetActive(false);
            _menuCanvas.gameObject.SetActive(false);
            _gameOverCanvas.gameObject.SetActive(true);
            _winnerText.text = "Right Player Win!";
        } else if (_scoreRight >= 5 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            _gameCanvas.gameObject.SetActive(false);
            _menuCanvas.gameObject.SetActive(false);
            _gameOverCanvas.gameObject.SetActive(true);
            _winnerText.text = "Left Player Win!";
        }
    }

    public void ScoreGoalLeft()
    {
        _scoreLeft++;
        UpdateUI();
        ResetBall();
    }
    public void ScoreGoalRight()
    {
        _scoreRight++;
        UpdateUI();
        ResetBall();
    }

    public void StartGame()
    {
        _ballController.Go();
    }

    public void ResetBall()
    {
        _ballController.Stop();
        _ball.transform.position = _startingBallPosition;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartGame();
            return;
        }
        _starter.StartCountdown();
    }
    
    private void UpdateUI()
    {
        _scoreTextLeft.text = _scoreLeft.ToString();
        _scoreTextRight.text = _scoreRight.ToString();
    }
}
