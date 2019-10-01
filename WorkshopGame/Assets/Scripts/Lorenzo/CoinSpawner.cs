using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Assets.Scripts.Lorenzo
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField]
        private Coin _coinPrefab;

        [SerializeField]
        private int _startAmount = 2;

        [SerializeField]
        private float _minDistanceFromPlayer = 5f;

        [SerializeField]
        private Transform[] _spawnPoints;

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
            Transform[] candidates = _spawnPoints.Where(t => !_spawnedCoinsAtPosition.ContainsValue(t)).ToArray();
            while (true)
            {
                Transform spawnPoint = candidates[UnityEngine.Random.Range(0, candidates.Length - 1)];
                if (Vector3.Distance(_player.position, spawnPoint.position) < _minDistanceFromPlayer)
                {
                    Debug.DrawRay(_player.position, spawnPoint.position, Color.red, 1f);
                    continue;
                }
                Coin coin = Instantiate(_coinPrefab, spawnPoint.position, Quaternion.identity);
                _spawnedCoinsAtPosition.Add(coin.gameObject, spawnPoint);
                coin.Triggered += CoinTouched;
                break;
            }
        }

        private void CoinTouched(object sender, EventArgs e)
        {
            if (!(sender is Coin coin))
                return;
            coin.Triggered -= CoinTouched;
            ScoreHolder.AddScore(10);
            _spawnedCoinsAtPosition.Remove(coin.gameObject);
            Destroy(coin.gameObject);
            SpawnNewCoin();
        }

    }
}
