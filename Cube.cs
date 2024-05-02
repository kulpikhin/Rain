using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Renderer))]
[RequireComponent (typeof(CubeCollisionHandler))]
public class Cube : MonoBehaviour
{
    private float _minWaitSecond = 2;
    private float _maxWaitSecond = 5;

    private CubeCollisionHandler _collisionHandler;
    private Renderer _renderer;
    private bool _isColorChanged;
    private Coroutine _waitCorutine;
    private WaitForSeconds _waitSeconds;
    private Color _baseColor;

    public event UnityAction<Cube> WorkDone;

    private void Awake()
    {
        _collisionHandler = GetComponent<CubeCollisionHandler>();
        _renderer = GetComponent<Renderer>();        
        _baseColor = _renderer.material.color;
    }

    private void OnEnable()
    {
        _isColorChanged = false;
        _renderer.material.color = _baseColor;
        _waitSeconds = new WaitForSeconds(GetRandomTime());
        _collisionHandler.GroundTouched += TouchGround;
    }

    private void OnDisable()
    {
        _collisionHandler.GroundTouched -= TouchGround;
    }

    private float GetRandomTime()
    {
        return Random.Range(_minWaitSecond, _maxWaitSecond + 1);
    }

    private void ChangeColor()
    {
        if(!_isColorChanged)
        {
            _renderer.material.color = new Color(Random.value, Random.value, Random.value);
            _isColorChanged = true;
        }
    }

    private void TouchGround()
    {
        ChangeColor();
        StartWaitCorutine();
    }

    private void StartWaitCorutine()
    {
        if (_waitCorutine != null)
        {
            StopCoroutine(_waitCorutine);
        }

        _waitCorutine = StartCoroutine(WaitCorutine());
    }

    private IEnumerator WaitCorutine()
    {
        yield return _waitSeconds;
        
        WorkDone?.Invoke(this);
    }
}
