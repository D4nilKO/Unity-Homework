using UnityEngine;

namespace Tasks.Advanced_Enemy_Generation
{
    [RequireComponent(typeof(MoverToTarget))]
    public class MoverByPoints : MonoBehaviour
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

        private void SetNextTarget()
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;

            _target = _points[_currentPoint];
        }
    }
}