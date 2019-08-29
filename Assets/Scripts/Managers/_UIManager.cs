using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _UIManager : MonoBehaviour
{
    // Manager References
    private _GameManager GM;

    [Header("Reference Images")]
    public Sprite enabledLife;
    public Sprite disabledLife;
    public Sprite enabledShield;
    public Sprite disabledShield;

    public List<GameObject> LivesIcons;
    public List<GameObject> ShieldsIcons;

    public TextMeshProUGUI MultiplierText, ScoreText, TimerText;
    private float totalMultiplier;
    private int score, time;

    private static _UIManager _instance;
    public static _UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        GM = _GameManager.Instance;

        if (!GM) { Debug.LogError("UIManager can't find the GameManager!"); }

        UpdateLivesIcons();
        UpdateShieldIcons();
    }

    private void Update()
    {
        // Get the latest multiplier and score
        if (GM)
        {
            totalMultiplier = GM.GetScoreManager().GetTotalMultiplier();
            score = Mathf.FloorToInt(GM.GetScoreManager().GetScore());
            time  = Mathf.FloorToInt(GM.GetComponent<Timer>().GetCurrentTime());
        }

        UpdateText();
    }

    private void UpdateText()
    {
        MultiplierText.text = "Multiplier: " + totalMultiplier.ToString("F2") + "x";
        ScoreText.text      = "Score: " + score.ToString();
        TimerText.text      = time.ToString();
    }

    private void UpdateLivesIcons()
    {
        if (GM)
        {
            int currentLives = GM.GetPlayer().GetComponent<PlayerStats>().GetLives();
            int currentSlot  = 0;

            foreach (var life in LivesIcons)
            {
                if (currentSlot < currentLives)
                    life.GetComponent<Image>().sprite = disabledLife;
                else
                    life.GetComponent<Image>().sprite = enabledLife;

                currentSlot++;
            }
        }
        else { Debug.LogError("Couldn't find the GameManager!"); }
    }

    private void UpdateShieldIcons()
    {
        if (GM)
        {
            int currentShields = GM.GetPlayer().GetComponent<PlayerStats>().GetShields();
            int currentSlot = 0;

            foreach (var shield in ShieldsIcons)
            {
                if (currentSlot < currentShields)
                    shield.GetComponent<Image>().sprite = disabledShield;
                else
                    shield.GetComponent<Image>().sprite = enabledShield;

                currentSlot++;
            }
        }
        else { Debug.LogError("Couldn't find the GameManager!"); }
    }

    public void UpdateIcons()
    {
        UpdateLivesIcons();
        UpdateShieldIcons();
    }
}
