using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5.0f;
    [SerializeField]
    private int _speedMultiplier = 2;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire;
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shield, _damageRight, _damageLeft;
    [SerializeField]
    private bool isTripleShotEnabled = false;
    [SerializeField]
    private bool isShieldEnabled = false;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private AudioSource _laserSound;

    private int _playerScore = 0;

    //User Input Variable
    public PlayerControls userInput;




    // Start is called before the first frame update
    void Start()
    {
        //Script Communication with null check
        //_spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>();
        //_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //_laserSound = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnMananger not assigned to Player");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UIManager not assigned to Player");
        }

        //take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, -3, 0);
        userInput = new PlayerControls();
        userInput.Gameplay.Enable();

        HideGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        MovementControl();

        FireLaser();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }


    void MovementControl()
    {
        //New Input System
        Vector3 moveDirection = userInput.Gameplay.Move.ReadValue<Vector2>();
        transform.position += moveDirection * _moveSpeed * Time.deltaTime;

        Debug.Log(moveDirection);

        //Game Boundaries - Player Location Based
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.9f, 4.9f), 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.6f, 9.6f), transform.position.y, 0);


        /*Old InputSystem 
            float xAxis = Input.GetAxis("Horizontal");
            float yAxis = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(xAxis, yAxis, 0);
            transform.Translate(movement * speed * Time.deltaTime);
            */

    }

    void FireLaser()
    {
        //New Input System
        float shootGun = userInput.Gameplay.Fire1.ReadValue<float>();

        //Gun Fire Rate and Gun Fire
        if (Mathf.Floor(shootGun) == 1.0f && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            if (isTripleShotEnabled == false)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0.002f, 0.8f, 0), Quaternion.identity);

            }
            else if (isTripleShotEnabled == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }

            _laserSound.Play();
        }

        /*Old Input System
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            }
            */
    }

    void HideGameObjects()
    {
        _shield.SetActive(false);
        _damageLeft.SetActive(false);
        _damageRight.SetActive(false);
    }

    public void DamageControl(int x)
    {
        if (isShieldEnabled == true)
        {
            _shield.SetActive(false);
            isShieldEnabled = false;
            return;
        }

        _health -= x;
        Debug.Log("Health is " + _health);
        _uiManager.UpdateLivesImg(_health);
        switch (_health)
        {
            case 2:
                _damageRight.SetActive(true);
                break;
            case 1:
                _damageLeft.SetActive(true);
                break;
            case 0:
                _spawnManager.PlayerDead();
                _uiManager.DisplayGameOver();
                Destroy(this.gameObject);
                break;
        }
    }

    public void UpdateScore(int score)
    {
        _playerScore += score;
        _uiManager.UpdateScore(_playerScore);
    }

    public void TripleShotEnable()
    {
        isTripleShotEnabled = true;
        StartCoroutine(TripleShotDisable());
    }

    public void SpeedEnable()
    {
        _moveSpeed *= _speedMultiplier;
        StartCoroutine(SpeedDisable());
    }

    public void ShieldEnable()
    {
        isShieldEnabled = true;
        _shield.SetActive(true);       
    }

    private IEnumerator TripleShotDisable()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotEnabled = false;

    }

    private IEnumerator SpeedDisable()
    {
        yield return new WaitForSeconds(5f);
        _moveSpeed /= _speedMultiplier;
    }
}
