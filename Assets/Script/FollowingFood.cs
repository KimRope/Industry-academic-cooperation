using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConveyerGame
{
    public class FollowingFood : MonoBehaviour
    {
        Vector2 StartLocation;
        public bool isClick;
        public SpriteRenderer ThisSprite;
        public Text scoreText;

        public CustomerManager CustomerManager_cs;
        public ConveyerManager ConveyerManager_cs;

        Collision2D wasteBasketCollision = null; //�������İ��� �浹ü
        Collision2D customerCollision = null; //������ �浹ü

        Color tmp;

        int speechFoodNum; //��ǳ�� ���Ĺ�ȣ
        int Score;

        public AudioSource correctAudio;
        public AudioSource incorrectAudio;
        public AudioSource trashAudio;
        private void Awake()
        {
            StartLocation = transform.position;
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            dragCustomer(); //������ ������ �巡���ϸ�

            mouseBtnUp(); //���콺 ��ư�� ����
            
            if (isClick) //Ŭ�������̸�
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePosition; //���콺 ��ġ�� ���󰣴�
            }
            if(Time.timeScale == 0f)
            {
                unClick();
            }
        }

        void dragCustomer()
        {
            if(customerCollision != null && customerCollision.gameObject.CompareTag("Customer"))
            {
                CustomerDragColor(customerCollision, 0.25f);
            }
        }
        void mouseBtnUp()
        {
            if (Input.GetMouseButtonUp(0)) //���콺�� ����
            {
                if (wasteBasketCollision != null) //������ �±װ� ������
                {
                    wasteBasketCollision = null;
                    ThisSprite.sprite = null;
                    trashAudio.Play();
                }
                else if (customerCollision != null) //Ư�� ������ ������ ������ �ָ�
                {
                    OnEnableCustomer OnEnCustomer = customerCollision.gameObject.GetComponent<OnEnableCustomer>(); //�� (Ȱ��ȭ)��ũ��Ʈ ���� 
                    speechFoodNum = OnEnCustomer.speechFoodNum;
                    OnEnCustomer.CusFood.sprite = ThisSprite.sprite;
                    if (ConveyerManager_cs.mainFoodNum == speechFoodNum) //�����ϴ� ������ ������
                    {
                        Score += OnEnCustomer.feedBackScore;
                        scoreText.text = "���� : " + Score;
                        OnEnCustomer.O_Animator.gameObject.SetActive(true);
                        correctAudio.Play();
                        Debug.Log("����");
                    }
                    else
                    {
                        OnEnCustomer.X_Animator.gameObject.SetActive(true);
                        incorrectAudio.Play();
                        Debug.Log("����");
                    }
                    customerCollision.gameObject.tag = "CustomerDone";
                    CustomerDragColor(customerCollision, 0f);
                    CustomerManager_cs.inActiveCustomers.Add(customerCollision.transform); //��Ȱ��ȭ ���� �ش� �� �߰�

                    OnEnCustomer.dIsableCustomer(); //�� ��Ȱ��ȭ

                    customerCollision = null;
                    OnEnCustomer = null;
                    ThisSprite.sprite = null;
                }
                unClick();
            }
        }

        void unClick()
        {
            isClick = false; //Ŭ������ ����
            transform.position = StartLocation; //ó�� ��ǥ�� �̵�
        }

        void CustomerDragColor(Collision2D customCol, float alpha) //�� �巡�� ���� �÷�����
        {
            if (customCol != null)
            {
                tmp = customCol.gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = alpha;
                customCol.gameObject.GetComponent<SpriteRenderer>().color = tmp;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (isClick) //Ŭ�������϶�
            {
                if (collision.gameObject.CompareTag("WasteBasket")) //�������İ� �±װ� ���˽�
                {
                    wasteBasketCollision = collision; //�浹���� ����
                }

                if (collision.gameObject.CompareTag("Customer")) //���� �±װ� ���˽�
                {
                    customerCollision = collision; //�浹���� ����
                }

            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (isClick) //Ŭ�������϶�
            {
                if (collision.gameObject.CompareTag("WasteBasket")) //�������İ� �±װ� �и���
                {
                    wasteBasketCollision = null; //�浹�и� ����
                }
                if (collision.gameObject.CompareTag("Customer")) //���� �±װ� �и���
                {
                    CustomerDragColor(customerCollision, 0f);
                    customerCollision = null; //�浹�и� ����
                }
            }
        }


        void OnApplicationFocus(bool hasFocus) //ȭ�鿡�� ������ (ALT + Tab)
        {
            if (!hasFocus && Time.timeScale != 0f)
            {
                unClick();
            }
        }

    }
}
