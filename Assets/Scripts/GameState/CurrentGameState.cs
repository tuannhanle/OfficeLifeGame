using System;
using UnityEngine;

[CreateAssetMenu(menuName = "OnlyOnce/CurrentGameState")]
public sealed class CurrentGameState : ScriptableObject
{
    [SerializeField] private GameState state = new GameState();

    public GameState Current => state;
    public void Init() => Init(new GameState());
    public void Init(GameState initialState) => state = initialState;
    public void Awake() => AddMoney(0);
    public void AddMoney(int amount) => UpdateState(g => g.Money += amount);
    //public void ChangeStage(GameStage gameStage) => UpdateState(g => g.gameStage = gameStage);

    //public void UpdateEnergy(float value) => UpdateState(g => g.Energy = value);

    public void UpdateState(Action<GameState> apply)
    {
        UpdateState(_ =>
        {
            apply(state);
            return state;
        });
    }

    public void UpdateState(Func<GameState, GameState> apply)
    {
        state = apply(state);
        Message.Publish(new GameStateChanged(state));
    }
}
