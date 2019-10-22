using UnityEngine;
using UnityEngine.UI;

public class ScoreHolder : MonoBehaviour
{
    private static int score;

    [SerializeField]
    private Text _text;

    private void Awake()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = score.ToString();
    }

    public static void AddScore(int amount) => score += amount;

}
