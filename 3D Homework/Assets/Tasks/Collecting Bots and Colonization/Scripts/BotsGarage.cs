using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    public class BotsGarage : MonoBehaviour
    {
        [SerializeField] private CollectingBot _collectingBotPrefab;
        [SerializeField] private Vector3 _botSpawnPoint;
        [SerializeField] private List<CollectingBot> _collectingBots = new();

        private float _botSpawnPointRadius = 0.5f;
        
        public int FreeBotsCount => _collectingBots.Count(bot => bot.IsFree);
        public int BotsCount => _collectingBots.Count;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position + _botSpawnPoint, _botSpawnPointRadius);
        }

        public bool TryGetFreeBot(out CollectingBot freeBot)
        {
            freeBot = _collectingBots.FirstOrDefault(bot => bot.IsFree);

            return freeBot != null;
        }
        
        public void AddBot(CollectingBot bot)
        {
            _collectingBots.Add(bot);
        }
        
        public void RemoveBot(CollectingBot bot)
        {
            _collectingBots.Remove(bot);
        }

        public void CreateBot()
        {
            CollectingBot bot = Instantiate(_collectingBotPrefab, transform.position + _botSpawnPoint,
                Quaternion.identity, transform);
            _collectingBots.Add(bot);
        }
    }
}