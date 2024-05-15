using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : FigureSpawner<Figure>
{
    private float _spawnCooldawn = 0.2f;
    private bool _isActive;
    private Coroutine _spawnCorutine;
    private WaitForSeconds _waitSeconds;
    private BoxCollider _boxCollider;    

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _waitSeconds = new WaitForSeconds(_spawnCooldawn);
    }

    private void Start()
    {
        StartSpawnCorutine();
    }

    private Vector3 GetRandomPosition()
    {
        Bounds bounds = _boxCollider.bounds;
        float zSpawnPosition = Random.Range(bounds.min.z, bounds.max.z);
        float xSpawnPosition = Random.Range(bounds.min.x, bounds.max.x);

        return new Vector3(xSpawnPosition, transform.position.y, zSpawnPosition);
    }

    private void StartSpawnCorutine()
    {
        if (_spawnCorutine != null)
        {
            StopCoroutine(_spawnCorutine);
        }

        _isActive = true;
        _spawnCorutine = StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (_isActive)
        {
            Figure cube = Spawn(GetRandomPosition());

            yield return _waitSeconds;
        }
    }
}
