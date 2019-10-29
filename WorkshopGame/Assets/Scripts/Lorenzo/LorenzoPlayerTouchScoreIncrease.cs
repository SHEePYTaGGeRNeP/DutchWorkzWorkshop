using UnityEngine;

namespace Assets.Scripts.Lorenzo
{
    public class LorenzoPlayerTouchScoreIncrease : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _coinPickupAudioSource;

        private void OnTriggerEnter(Collider other)
        {
            Coin c = other.GetComponent<Coin>();
            if (c is null)
                return;
            ScoreHolder.AddScore(10);
            this._coinPickupAudioSource.Play();
        }
    }
}