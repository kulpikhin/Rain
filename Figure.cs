using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Figure : MonoBehaviour
{
    protected float _duration;
    protected WaitForSeconds _waitSeconds;

    private float _minWaitSecond = 2;
    private float _maxWaitSecond = 5;
    private Coroutine _waitCorutine;

    public event Action<Figure> WorkDone;

    protected virtual void OnEnable()
    {
        _duration = GetRandomTime();
        _waitSeconds = new WaitForSeconds(_duration);
    }

    protected void StartWaitCorutine()
    {
        if (_waitCorutine != null)
        {
            StopCoroutine(_waitCorutine);
        }

        _waitCorutine = StartCoroutine(Waiting());
    }

    protected virtual void WorkFinishing()
    {
        WorkDone?.Invoke(this);
    }

    private float GetRandomTime()
    {
        return Random.Range(_minWaitSecond, _maxWaitSecond + 1);
    }

    private IEnumerator Waiting()
    {
        yield return _waitSeconds;

        WorkFinishing();
    }
}
