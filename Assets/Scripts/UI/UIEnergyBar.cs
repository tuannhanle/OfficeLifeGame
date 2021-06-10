using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Observer;

namespace MyGame
{
    public class  UIEnergyBar : MonoBehaviour
    {
        private Image image;

        void Awake()
        {
            image = GetComponent<Image>();
            this.RegisterListener(EventID.OnEnergyUpdate, (energy) =>
            {

                if ((float)energy <= 100f)
                {
                    FillAmound((float)energy / 100f);

                }
                else
                {
                    FillAmound(1f);
                }
            });
        }

        public void FillAmound(float percent)
        {
            image.fillAmount = percent;
        }


    }
}