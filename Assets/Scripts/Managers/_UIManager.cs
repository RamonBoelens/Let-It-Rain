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
    public List<Sprite> lifeIcons;
    public List<Sprite> shieldIcons;
    public List<Sprite> invulnerabilityIcons;
    public List<Sprite> doublePointsIcons;

    [Space(20)]
    public List<GameObject> lifeIconSlots;
    public List<GameObject> shieldIconSlots;
    public Image invulnerabilitySlot;
    public Image doublePointsSlot;

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

        // Check if the icon slots are set in the inspector
        if (lifeIconSlots.Count <= 0) { Debug.LogWarning("Can't find the life icon slots!"); }
        if (shieldIconSlots.Count <= 0) { Debug.LogWarning("Can't find the shield icon slots!"); }

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

            foreach (var life in lifeIconSlots)
            {
                if (currentSlot < currentLives)
                    life.GetComponent<Image>().sprite = lifeIcons[1];
                else
                    life.GetComponent<Image>().sprite = lifeIcons[0];

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

            foreach (var shield in shieldIconSlots)
            {
                if (currentSlot < currentShields)
                    shield.GetComponent<Image>().sprite = shieldIcons[1];
                else
                    shield.GetComponent<Image>().sprite = shieldIcons[0];

                currentSlot++;
            }
        }
        else { Debug.LogError("Couldn't find the GameManager!"); }
    }

    private void UpdatePowerUpIcons()
    {
        if (GM)
        {
            if (GM.GetScoreManager().GetDoublePointsActive())
                doublePointsSlot.sprite = doublePointsIcons[1];
            else
                doublePointsSlot.sprite = doublePointsIcons[0];
        }

        if (GM)
        {
            if (GM.GetPlayer().GetComponent<PlayerStats>().GetInvulnerability())
                invulnerabilitySlot.sprite = invulnerabilityIcons[1];
            else
                invulnerabilitySlot.sprite = invulnerabilityIcons[0];
        }
    }

    public void UpdateIcons()
    {
        UpdateLivesIcons();
        UpdateShieldIcons();
        UpdatePowerUpIcons();
    }
}
