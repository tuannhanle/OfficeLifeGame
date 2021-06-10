using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;
using System;
using Observer;

namespace MyGame
{

    public enum GameStage { StandBy, Playing, End }

    public class GameManager : PersistentSingleton<GameManager>
    {
        public CurrentGameState currentGameState;

        public DevMode devMode;


        public Action OnGameIndex, OnGamePlaying, OnGameEnd;

        protected override void Awake()
        {
            currentGameState.Awake();

            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            // Tạm thời WIN/LOSE => END GAME (như nhau)
            this.RegisterListener(EventID.OnGameLose, (o) => ChangeStage(GameStage.End));
            this.RegisterListener(EventID.OnGameWin, (o) => ChangeStage(GameStage.End));
            this.RegisterListener(EventID.OnDeathItemErned, (o) => this.PostEvent(EventID.OnGameLose));

            ChangeStage(GameStage.StandBy);
            base.Awake();
        }

        public void ChangeStage(GameStage gameStage)
        {
            Debug.Log("GAME STATE: " + gameStage);

            this.PostEvent(EventID.OnCastCollider, gameStage);


            switch (gameStage)
            {

                case GameStage.StandBy:
                    this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Idle);
                    this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Freeze);

                    //OnCostEnergy -= CostEnegryDuringWaiting;
                    break;

                case GameStage.Playing:
                    this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Run);
                    this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Run);
                    break;

                case GameStage.End:
                    this.PostEvent(EventID.OnCastAnim, PlayerAnimState.Idle);
                    this.PostEvent(EventID.OnCastMovementState, PlayerMomvementState.Freeze);


                    break;
                default:
                    break;
            }


        }





        //public void ChangeStage(GameStage gameStage)
        //{
        //    currentGameState.ChangeStage(gameStage);
        //    // them switch case để xử lý transmit giữa 2 stage sau này
        //    // ví dụ hiển thị bảng điểm hoặc fade out

        //}
    }
}