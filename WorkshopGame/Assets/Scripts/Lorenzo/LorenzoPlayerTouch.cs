using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Lorenzo
{
    public class LorenzoPlayerTouch : MonoBehaviour
    {
        public GameObjectUnityEvent CoinTouched;

        private void OnCollisionEnter(Collision collision)
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e is null)
                return;
            Debug.Log("Player died.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnTriggerEnter(Collider other)
        {
            Coin c = other.GetComponent<Coin>();
            if (c is null)
                return;
            CoinTouched?.Invoke(c.gameObject);
        }
    }
}