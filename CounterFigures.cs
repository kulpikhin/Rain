using TMPro;
using UnityEngine;

public class CounterFigures : MonoBehaviour
{
    [SerializeField] PoolFigures<Figure> _poolCubes;
    [SerializeField] PoolFigures<Figure> _poolBombs;
    [SerializeField] FigureSpawner<Figure> _cubeSpawner;
    [SerializeField] FigureSpawner<Figure> _bombsSpawner;
    [SerializeField] TextMeshProUGUI _bombsText;
    [SerializeField] TextMeshProUGUI _cubesText;
    [SerializeField] TextMeshProUGUI _countText;

    private int _totalBombs = 0;
    private int _totalCubes = 0;
    private int _countFigures = 0;

    private void FixedUpdate()
    {
        ChangeFigureCount();
    }

    private void OnEnable()
    {
        _cubeSpawner.FigureSpawned += OnCubeSpawned;
        _bombsSpawner.FigureSpawned += OnBombSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.FigureSpawned -= OnCubeSpawned;
        _bombsSpawner.FigureSpawned -= OnBombSpawned;
    }

    private void OnCubeSpawned(Figure figure)
    {
        _totalCubes++;
        _cubesText.text = "Cubes: " + _totalCubes;
    }

    private void OnBombSpawned(Figure figure)
    {
        _totalBombs++;
        _bombsText.text = "Bombs: " + _totalBombs;
    }

    private void ChangeFigureCount()
    {
        _countFigures = _poolCubes.GetFigureCount() + _poolBombs.GetFigureCount();
        _countText.text = "Active Figures: " + _countFigures;
    }
}
