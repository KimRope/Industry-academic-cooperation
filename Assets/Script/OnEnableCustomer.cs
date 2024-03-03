using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyerGame
{
    public class OnEnableCustomer : MonoBehaviour //����� Ȱ��ȭ �� ��(OnEnable)
    {
        public ConveyerManager conveyerManager_cs; //�����̾� ��ũ��Ʈ
        public SpriteRenderer speechFoodSprite; //���ϴ� ���� ��������Ʈ
        public CustomerManager CustomerManager_cs;

        public int speechFoodNum;
        public SpriteRenderer ratingFaceSprite; //�� ��������Ʈ
        public SpriteRenderer HumanSprite; //���� ��������Ʈ
        public SpriteRenderer CusFood; //�ֹ� ���� ���� ��������Ʈ 

        public Transform X_Animator;
        public Transform O_Animator;

        int randFood; //���� ����
        int randHumanSp; //��� �̹��� ����

        float feedBackMax = 20f;
        float feedBackTime ;
        public int feedBackScore;

        public AudioSource OutSound;

        bool isStop; //�ð��� ����
        private void OnEnable()
        {
            isStop = false;
            X_Animator.gameObject.SetActive(false);
            O_Animator.gameObject.SetActive(false);
            feedBackTime = feedBackMax;
            randFood = Random.Range(0, conveyerManager_cs.defaultFood.Length); //�⺻���� ����
            randHumanSp = Random.Range(0, CustomerManager_cs.customerSprite.Length); //��� �̹��� ����
            HumanSprite.sprite = CustomerManager_cs.customerSprite[randHumanSp]; //��� �̹��� ����
            speechFoodSprite.sprite = conveyerManager_cs.defaultFood[randFood];//��ǳ�� ���Ŀ� �������� ����
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
                    CustomerManager_cs.inActiveCustomers.Add(transform); //��Ȱ��ȭ ���� ���� �߰�
                    transform.gameObject.SetActive(false);
                }
            }
        }

        public void dIsableCustomer()//1.5�� �� �� ��Ȱ��ȭ
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
