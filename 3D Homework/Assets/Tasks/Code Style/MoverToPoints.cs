using UnityEngine;

public class MoverToPoints : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _path;

    private Transform[] _points;
    private int _currentPoint;

    private Transform _target;

    private void Start()
    {
        SetPoints();
        
        _target = _points[_currentPoint];
    }

    public void Update()
    {
        Move();
        
        if (transform.position != _target.position)
            return;

        SetNextTarget();
    }

    private void SetPoints()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void SetNextTarget()
    {
        _currentPoint++;

        if (_currentPoint >= _points.Length)
            _currentPoint = 0;

        _target = _points[_currentPoint];
    }
}