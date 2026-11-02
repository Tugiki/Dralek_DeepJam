
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;
    private int comboMultiplier = 1;
    private const int maxComboMultiplier = 10;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        UpdateUI();
    }

    public void AddCoinScore(int basePoints)
    {
        int earnedPoints = basePoints * comboMultiplier;
        score += earnedPoints;

        Debug.Log("Kazanýlan puan: " + earnedPoints);
        Debug.Log("Toplam skor: " + score);
        Debug.Log("Combo: " + comboMultiplier + "x");

        comboMultiplier = Mathf.Min(
            comboMultiplier + 1,
            maxComboMultiplier
        );

        UpdateUI();
    }

    public void ResetCombo()
    {
        comboMultiplier = 1;
        Debug.Log("Hasar alýndý! Combo sýfýrlandý: 1x");

        UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetComboMultiplier()
    {
        return comboMultiplier;
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skor: " + score;
        }

        if (comboText != null)
        {
            comboText.text = "Combo: " + comboMultiplier + "x";
        }
    }
}
