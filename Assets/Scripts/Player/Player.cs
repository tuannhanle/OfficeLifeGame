using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

namespace MyGame
{
    public class Player :MonoBehaviour
    {

        [SerializeField] private float _costEnergyWhenWaiting;
        [SerializeField] private float _energy; //khi nao nen set la 100f
        private const float _maxEnergy = 100f;
        public Action  OnCostEnergy;

        private PlayerLevel _playerLevel=null;

        void Awake()
        {
            _playerLevel = GetComponent<PlayerLevel>();

            OnCostEnergy += CostEnegryDuringWaiting;

           

            this.RegisterListener(EventID.OnCoffeeErned, (value) => this.PostEvent(EventID.OnEnergyUpdate, AddEnergy( (float)value)));
            this.RegisterListener(EventID.OnMoneyErned, (value) => GameManager.instance.currentGameState.AddMoney ((int)value));
            this.RegisterListener(EventID.OnAreaStayCollided, (areaType) =>
            {
                switch ((AreaType)areaType)
                {
                    case AreaType.AreaUp:
                        this.PostEvent(EventID.OnEnergyUpdate, AddEnergy(Time.deltaTime*4f));
                        break;
                    case AreaType.AreaDown:
                        this.PostEvent(EventID.OnEnergyUpdate, AddEnergy(-Time.deltaTime * 4f));
                        break;
                    default:
                        break;
                }
            });
            this.RegisterListener(EventID.OnCostEnergy, (o) => { OnCostEnergy?.Invoke(); });
            this.RegisterListener(EventID.OnEnergyUpdate, (o) => { var energy = (float)o; Debug.Log("here");
                if (energy > 75 )
                    _playerLevel.SetItOn(PlayerLevelState.Senior);
                if (energy <=75 && energy>50)
                    _playerLevel.SetItOn(PlayerLevelState.Junior);
                if (energy <= 50 && energy > 25)
                    _playerLevel.SetItOn(PlayerLevelState.Fresher);
                if (energy < 25)
                    _playerLevel.SetItOn(PlayerLevelState.Intern);
            });

            this.PostEvent(EventID.OnEnergyUpdate, AddEnergy(0f));
        }

        private float AddEnergy(float amout)
        {
            _energy += amout;

            if (_energy>=_maxEnergy)
            {
                _energy = _maxEnergy;

            }
            if (_energy <= 0)
            {
                _energy = 0f;
                this.PostEvent(EventID.OnGameLose);
            }

            return _energy;
        }
        private void CostEnegryDuringWaiting()
        {      
            this.PostEvent(EventID.OnEnergyUpdate, AddEnergy(-Time.deltaTime * 8f));
        }



    }
}