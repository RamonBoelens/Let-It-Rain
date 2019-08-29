using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    // Manager References
    private _GameManager GM;

    private void Start()
    {
        GM = _GameManager.Instance;
        if (!GM) { Debug.LogError("UIManager can't find the GameManager!"); }
    }

    public void ExtraLife()
    {
        GM.GetPlayer().GetComponent<PlayerStats>().AddLife();
        GM.GetUIManager().UpdateIcons();
    }

    public void ExtraShield()
    {
        GM.GetPlayer().GetComponent<PlayerStats>().AddShield();
        GM.GetUIManager().UpdateIcons();
    }
}
