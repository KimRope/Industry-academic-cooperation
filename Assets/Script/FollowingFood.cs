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

        Collision2D wasteBasketCollision = null; //메인음식과의 충돌체
        Collision2D customerCollision = null; //고객과의 충돌체

        Color tmp;

        int speechFoodNum; //말풍선 음식번호
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
            dragCustomer(); //고객에게 음식을 드래그하면

            mouseBtnUp(); //마우스 버튼을 땔때
            
            if (isClick) //클릭상태이면
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePosition; //마우스 위치를 따라간다
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
            if (Input.GetMouseButtonUp(0)) //마우스를 때면
            {
                if (wasteBasketCollision != null) //휴지통 태그가 있으면
                {
                    wasteBasketCollision = null;
                    ThisSprite.sprite = null;
                    trashAudio.Play();
                }
                else if (customerCollision != null) //특정 고객에게 음식을 가져다 주면
                {
                    OnEnableCustomer OnEnCustomer = customerCollision.gameObject.GetComponent<OnEnableCustomer>(); //고객 (활성화)스크립트 정보 
                    speechFoodNum = OnEnCustomer.speechFoodNum;
                    OnEnCustomer.CusFood.sprite = ThisSprite.sprite;
                    if (ConveyerManager_cs.mainFoodNum == speechFoodNum) //대응하는 음식이 맞으면
                    {
                        Score += OnEnCustomer.feedBackScore;
                        scoreText.text = "점수 : " + Score;
                        OnEnCustomer.O_Animator.gameObject.SetActive(true);
                        correctAudio.Play();
                        Debug.Log("정답");
                    }
                    else
                    {
                        OnEnCustomer.X_Animator.gameObject.SetActive(true);
                        incorrectAudio.Play();
                        Debug.Log("오답");
                    }
                    customerCollision.gameObject.tag = "CustomerDone";
                    CustomerDragColor(customerCollision, 0f);
                    CustomerManager_cs.inActiveCustomers.Add(customerCollision.transform); //비활성화 고객에 해당 고객 추가

                    OnEnCustomer.dIsableCustomer(); //고객 비활성화

                    customerCollision = null;
                    OnEnCustomer = null;
                    ThisSprite.sprite = null;
                }
                unClick();
            }
        }

        void unClick()
        {
            isClick = false; //클릭상태 해제
            transform.position = StartLocation; //처음 좌표로 이동
        }

        void CustomerDragColor(Collision2D customCol, float alpha) //고객 드래그 관련 컬러변경
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
            if (isClick) //클릭상태일때
            {
                if (collision.gameObject.CompareTag("WasteBasket")) //메인음식과 태그가 접촉시
                {
                    wasteBasketCollision = collision; //충돌접촉 저장
                }

                if (collision.gameObject.CompareTag("Customer")) //고객과 태그가 접촉시
                {
                    customerCollision = collision; //충돌접촉 저장
                }

            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (isClick) //클릭상태일때
            {
                if (collision.gameObject.CompareTag("WasteBasket")) //메인음식과 태그가 분리시
                {
                    wasteBasketCollision = null; //충돌분리 저장
                }
                if (collision.gameObject.CompareTag("Customer")) //고객과 태그가 분리시
                {
                    CustomerDragColor(customerCollision, 0f);
                    customerCollision = null; //충돌분리 저장
                }
            }
        }


        void OnApplicationFocus(bool hasFocus) //화면에서 나가면 (ALT + Tab)
        {
            if (!hasFocus && Time.timeScale != 0f)
            {
                unClick();
            }
        }

    }
}
