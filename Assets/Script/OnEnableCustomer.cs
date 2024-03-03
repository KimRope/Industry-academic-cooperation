using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyerGame
{
    public class OnEnableCustomer : MonoBehaviour //사람이 활성화 될 때(OnEnable)
    {
        public ConveyerManager conveyerManager_cs; //컨베이어 스크립트
        public SpriteRenderer speechFoodSprite; //원하는 음식 스프라이트
        public CustomerManager CustomerManager_cs;

        public int speechFoodNum;
        public SpriteRenderer ratingFaceSprite; //평가 스프라이트
        public SpriteRenderer HumanSprite; //본인 스프라이트
        public SpriteRenderer CusFood; //주문 나온 음식 스프라이트 

        public Transform X_Animator;
        public Transform O_Animator;

        int randFood; //랜덤 음식
        int randHumanSp; //사람 이미지 랜덤

        float feedBackMax = 20f;
        float feedBackTime ;
        public int feedBackScore;

        public AudioSource OutSound;

        bool isStop; //시간평가 중지
        private void OnEnable()
        {
            isStop = false;
            X_Animator.gameObject.SetActive(false);
            O_Animator.gameObject.SetActive(false);
            feedBackTime = feedBackMax;
            randFood = Random.Range(0, conveyerManager_cs.defaultFood.Length); //기본음식 랜덤
            randHumanSp = Random.Range(0, CustomerManager_cs.customerSprite.Length); //사람 이미지 랜덤
            HumanSprite.sprite = CustomerManager_cs.customerSprite[randHumanSp]; //사람 이미지 변경
            speechFoodSprite.sprite = conveyerManager_cs.defaultFood[randFood];//말풍선 음식에 랜덤음식 변경
            CusFood.sprite = null;
            speechFoodNum = randFood;
            gameObject.tag = "Customer";
        }

        private void Update()
        {
            if (!isStop)
            {
                feedBackTime -= Time.deltaTime;
                if (feedBackTime > 11f)
                {
                    ratingFaceSprite.sprite = CustomerManager_cs.feedBackFaceSprite[0];
                    feedBackScore = 1000;
                }
                else if (feedBackTime > 5f)
                {
                    ratingFaceSprite.sprite = CustomerManager_cs.feedBackFaceSprite[1];
                    feedBackScore = 500;
                }
                else if (feedBackTime > 0f)
                {
                    ratingFaceSprite.sprite = CustomerManager_cs.feedBackFaceSprite[2];
                    feedBackScore = 200;
                }
                else
                {
                    OutSound.Play();
                    CustomerManager_cs.inActiveCustomers.Add(transform); //비활성화 고객에 본인 추가
                    transform.gameObject.SetActive(false);
                }
            }
        }

        public void dIsableCustomer()//1.5초 뒤 고객 비활성화
        {
            isStop = true;
            Invoke("Done", 1.5f);
        }

        void Done()
        {
            gameObject.SetActive(false);
        }
    }
}
