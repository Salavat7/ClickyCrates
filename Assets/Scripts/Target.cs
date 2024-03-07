using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private int _pointValue;
    private Rigidbody _targetRb;
    private GameManager _gameManager;
    private float _minSpeed = 12;
    private float _maxSpeed = 15;
    private float _maxTorque = 10;
    private float _xRange = 4;
    private float _ySpawnPos = -2;
    private float _zSpawnPos = 0;
    private int _lifeIncrement = -1;

    private void Start()
    {
        _targetRb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    public void DestroyTarget()
    {
        if (_gameManager.IsGameActive)
        {
            Destroy(gameObject);
            Instantiate(_explosionParticle, transform.position, transform.rotation);
            _gameManager.UpdateScore(_pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad") && _gameManager.IsGameActive)
        {
            _gameManager.UpdateLives(_lifeIncrement);
        }
        Destroy(gameObject);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos, _zSpawnPos);
    }
}
