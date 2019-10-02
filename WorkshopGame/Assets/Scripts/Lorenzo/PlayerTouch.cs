using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Lorenzo
{
    public class PlayerTouch : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e is null)
                return;
            Debug.Log("Player died.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}