
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    private const int maxScores = 10;
    private const string scoreKey = "LeaderboardScore_";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else    
        {
            Destroy(gameObject);
        }
    }

    public void SaveScore(int newScore)
    {
        int[] scores = new int[maxScores];

        for (int i = 0; i < maxScores; i++)
        {
            scores[i] = PlayerPrefs.GetInt(scoreKey + i, 0);
        }

        for (int i = 0; i < maxScores; i++)
        {
            if (newScore > scores[i])
            {
                for (int j = maxScores - 1; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                }

                scores[i] = newScore;
                break;
            }
        }

        for (int i = 0; i < maxScores; i++)
        {
            PlayerPrefs.SetInt(scoreKey + i, scores[i]);
        }

        PlayerPrefs.Save();

        Debug.Log("Skor liderlik tablosuna kaydedildi: " + newScore);
    }

    public int GetScoreAt(int index)
    {
        if (index < 0 || index >= maxScores)
        {
            return 0;
        }

        return PlayerPrefs.GetInt(scoreKey + index, 0);
    }
}
