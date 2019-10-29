using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Lorenzo
{
    public class LorenzoCoinSpawner : MonoBehaviour
    {
        [SerializeField]
        private Coin _coinPrefab;

        [SerializeField]
        [Tooltip("How many coins spawn at the beginning of the game")]
        private int _startAmount = 2;

        [SerializeField]
        [Tooltip("What is the minimum distance from the player, so we don't spawn a new coin right next to it")]
        private float _minDistanceFromPlayer = 5f;

        [SerializeField]
        [Tooltip("All possible coin spawn locations")]
        private Transform[] _spawnPoints;

        /// <summary>A dictionary that keeps track of where coins are currently spawned</summary>
        private Dictionary<GameObject, Transform> _spawnedCoinsAtPosition = new Dictionary<GameObject, Transform>();

        [SerializeField]
        private Transform _player;

        private void Awake()
        {
            for (int i = 0; i < _startAmount; i++)
                SpawnNewCoin();
        }

        public void SpawnNewCoin()
        {
            // get all empty spawn locations
            Transform[] candidates = _spawnPoints.Where(t => !_spawnedCoinsAtPosition.ContainsValue(t)).ToArray();
            while (true)
            {
                Transform spawnPoint = candidates[UnityEngine.Random.Range(0, candidates.Length - 1)];
                if (Vector3.Distance(_player.position, spawnPoint.position) < _minDistanceFromPlayer)
                {
                    // for fun. Draw a ray when took a position that is too close to the player
                    Debug.DrawRay(_player.position, spawnPoint.position, Color.cyan, 1f);
                    continue;
                }
                // actually create / instantiate the coin in the scene.
                Coin coin = Instantiate(_coinPrefab, spawnPoint.position, Quaternion.identity);
                _spawnedCoinsAtPosition.Add(coin.gameObject, spawnPoint);
                break;
            }
        }

        public void CoinTouched(GameObject coin)
        {
            ScoreHolder.AddScore(10);
            _spawnedCoinsAtPosition.Remove(coin);
            Destroy(coin);
            SpawnNewCoin();
        }

    }
}
