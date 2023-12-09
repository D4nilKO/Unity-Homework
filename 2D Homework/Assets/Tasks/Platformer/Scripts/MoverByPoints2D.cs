using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(MoverToTarget))]
    public class MoverByPoints2D : MonoBehaviour
    {
        [SerializeField] private Transform _path;

        private Transform[] _points;
        private int _currentPoint;

        private MoverToTarget _moverToTarget;
        private Transform _target;

        private void Awake()
        {
            _moverToTarget = GetComponent<MoverToTarget>();

            SetPoints();

            _target = _points[_currentPoint];
            _moverToTarget.SetTarget(_target);
        }

        private void Update()
        {
            if (_moverToTarget.IsReached)
                SetNextTarget();
        }

        private void SetPoints()
        {
            _points = new Transform[_path.childCount];

            for (int i = 0; i < _path.childCount; i++)
            {
                _points[i] = _path.GetChild(i);
                _points[i].transform.position = new Vector3(_points[i].position.x, _points[i].position.y, 0);
            }
        }

        private void SetNextTarget()
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;

            _target = _points[_currentPoint];
            _moverToTarget.SetTarget(_target);
        }
    }
}