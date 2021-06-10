using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using System.Linq;

namespace MyGame
{

    public enum ItemType { Coffee, DeathItem, Money }
    public class Item : MonoBehaviour, ITriggerable, IAudible
    {
        [SerializeField] private ItemType _itemType;
        // Start is called before the first frame update
        void Awake()
        {
            switch (_itemType)
            {
                case ItemType.Coffee:

                    break;
                case ItemType.DeathItem:
                    break;
                case ItemType.Money:
                    break;
                default:
                    break;
            }
        }

        public void EnterCollided()
        {
            GetComponentInChildren<ParticleSystem>()?.Play(true);
            //GetComponentInChildren<AudioSource>()?.Play();
            GetComponent<Animator>()?.Play("Collided");
            GetComponent<Collider>().enabled = false;

            switch (_itemType)
            {
                case ItemType.Coffee:
                    this.PostEvent(EventID.OnCoffeeErned, 5f); // kiem 1 cai data struct nao do de set cho 1f nay`
                    break;
                case ItemType.DeathItem:
                    this.PostEvent(EventID.OnDeathItemErned);
                    break;
                case ItemType.Money:
                    this.PostEvent(EventID.OnMoneyErned, 1);
                    break;
                default:
                    break;
            }
        }


        public void ExitCollided()
        {

        }

        public void StayCollided()
        {

        }

        public void SelfDestroy()
        {
            Destroy(this.gameObject);
        }


        public void SetVisible()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        public void PlayAudio()
        {
            GetComponentInChildren<AudioSource>()?.Play();

        }

        public void StopAudio()
        {
            GetComponentInChildren<AudioSource>()?.Stop();

        }
    }
}