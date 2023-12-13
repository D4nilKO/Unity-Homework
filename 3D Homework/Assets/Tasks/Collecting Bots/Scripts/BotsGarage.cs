using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class BotsGarage : MonoBehaviour
    {
        [SerializeField] private CollectingBot _collectingBotPrefab;
        [SerializeField] private Vector3 _centerPoint;
        [SerializeField] private List<CollectingBot> _collectingBots = new();

        private float _centerSphereRadius = 0.5f;

        private void Start()
        {
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position + _centerPoint, _centerSphereRadius);
        }

        public bool TryGetFreeBot(out CollectingBot freeBot)
        {
            freeBot = _collectingBots.FirstOrDefault(bot => bot.IsFree);

            return freeBot != null;
        }

        public int GetFreeBotsCount()
        {
            return _collectingBots.Count(bot => bot.IsFree);
        }

        public void CreateBot()
        {
            CollectingBot bot = Instantiate(_collectingBotPrefab, transform.position + _centerPoint,
                Quaternion.identity, transform);
            _collectingBots.Add(bot);
        }
    }
}