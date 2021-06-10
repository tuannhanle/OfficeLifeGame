using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIMoney : OnMessage<GameStateChanged>
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _text=GetComponentInChildren<TextMeshProUGUI>();
    }



    protected override void Execute(GameStateChanged msg)
    {
        _text.text = msg.State.Money.ToString();
    }
}
