using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public bool IOplayer;
    public int score;

    public string playerName;
    public TextMeshProUGUI nameTag;

    public ParticleSystem DangerAura;

    public Color playerColor;
    private void Start()
    {
        score = 0;
    }

    public void SetName(string nameValue)
    {
        if (IOplayer)
        {
            nameTag.text = nameValue;
            playerName = nameValue;
        }
        else
        {
            playerName = "You";
        }
    }
}
