using System.Collections;
using UnityEngine;
using System;

public class Figure : MonoBehaviour
{
    private float _minWaitSecond = 2;
    private float _maxWaitSecond = 5;

    private Coroutine _waitCorutine;
    private WaitForSeconds _waitSeconds;
    private float _lifeTime;

    public event Action<Figure> WorkDone;

    protected virtual void OnEnable()
    {
        _lifeTime = GetRandomTime();
        _waitSeconds = new WaitForSeconds(_lifeTime);
    }

    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(_minWaitSecond, _maxWaitSecond + 1);
    }

    protected void StartWaitCorutine()
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

        Debug.Log(_lifeTime);
        WorkDone?.Invoke(this);
    }
}
