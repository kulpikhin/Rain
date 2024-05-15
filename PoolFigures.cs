using UnityEngine;
using UnityEngine.Pool;

public class PoolFigures<T> : MonoBehaviour where T : Figure
{
    [SerializeField] private T _figurePrefab;

    private int _poolCapacity = 40;
    private int _poolMaxSize = 40;
    private ObjectPool<T> _poolFigures;

    private void Awake()
    {
        _poolFigures = new ObjectPool<T>(
        createFunc: () => Instantiate(_figurePrefab),
        actionOnGet: (figure) => ActionOnGet(figure),
        actionOnRelease: (figure) => ActionOnRelease(figure),
        actionOnDestroy: (figure) => Destroy(figure),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public T GetFigure()
    {
        return _poolFigures.Get();
    }

    public int GetFigureCount()
    {
        return _poolFigures.CountActive;
    }

    private void TempActionOnRelease(Figure figure)
    {
        _poolFigures.Release(figure as T);
    }

    private void ActionOnRelease(T figure)
    {
        figure.WorkDone -= TempActionOnRelease;
        figure.gameObject.SetActive(false);
    }

    private T ActionOnGet(T figure)
    {
        figure.WorkDone += TempActionOnRelease;
        figure.gameObject.SetActive(true);
        return figure;
    }
}
