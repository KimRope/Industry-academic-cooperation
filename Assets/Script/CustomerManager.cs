using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyerGame
{
    public class CustomerManager : MonoBehaviour
    {
        public Transform[] Customers;
        public Sprite[] customerSprite; //고객 이미지들
        public List<Transform> inActiveCustomers; //비활성화 고객
        public Sprite[] feedBackFaceSprite;


        float randTime;
        float createTime = 0;
        int randCus;

        public float randTimeMin;
        public float randTimeMax;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < Customers.Length; i++)
            {
                inActiveCustomers.Add(Customers[i]);
            }
            randTime = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            createCustomer();
            
        }

        void createCustomer() //비활성화 고객을 활성화
        {
            if (inActiveCustomers.Count != 0) //비활성화 고객이 있으면
            {
                if (randTime == 0f) //랜덤 생성시간 초기화시
                {
                    randTime = Random.Range(randTimeMin, randTimeMax); //생성시간 재정의
                }

                createTime += Time.deltaTime;

                if (createTime > randTime) //생성시간이 되면
                {
                    randCus = Random.Range(0, inActiveCustomers.Count); //비활성화 고객중 하나 선택
                    inActiveCustomers[randCus].gameObject.SetActive(true);//선택된 고객 활성화
                    inActiveCustomers.Remove(inActiveCustomers[randCus]);//활성화된 고객 리스트 제거

                    randTime = 0f; //생성시간 초기화 
                    createTime = 0f;
                }
            }
        }
    }
}

