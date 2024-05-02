using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private int _poolCapacity = 35;
    private int _poolMaxSize = 35;
    private float _spawnCooldawn = 0.2f;

    private float _minScpopeX = 111f;
    private float _maxScpopeX = 140f;
    private float _minScpopeZ = 99f;
    private float _maxScpopeZ = 125f;
    private float _baseHeight = 11f;

    private bool _isActive;
    private Coroutine _spawnCorutine;
    private WaitForSeconds _waitSeconds;
    private ObjectPool<Cube> _poolCubes;

    private void Awake()
    {
        _waitSeconds = new WaitForSeconds(_spawnCooldawn);
        _poolCubes = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab),
        actionOnGet: (cube) => ActionOnGet(cube),
        actionOnRelease: (cube) => ActionOnRelease(cube),
        actionOnDestroy: (cube) => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartSpawnCorutine();
    }

    private void ActionOnGet(Cube cube)
    {
        cube.WorkDone += _poolCubes.Release;
        cube.gameObject.SetActive(true);
        cube.transform.position = GetRandomPosition();
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.WorkDone -= _poolCubes.Release;
        cube.gameObject.SetActive(false);
    }

    private Vector3 GetRandomPosition()
    {
        float randomPointX = Random.Range(_minScpopeX, _maxScpopeX);
        float randomPointZ = Random.Range(_minScpopeZ, _maxScpopeZ);

        return new Vector3(randomPointX, _baseHeight, randomPointZ);
    }

    private void StartSpawnCorutine()
    {
        if (_spawnCorutine != null)
        {
            StopCoroutine(_spawnCorutine);
        }

        _isActive = true;
        _spawnCorutine = StartCoroutine(SpawnCorutine());
    }

    private IEnumerator SpawnCorutine()
    {
        while (_isActive)
        {
            _poolCubes.Get();

            yield return _waitSeconds;
        }
    }
}
