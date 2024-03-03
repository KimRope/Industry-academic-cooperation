using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyerGame
{
    public class CustomerManager : MonoBehaviour
    {
        public Transform[] Customers;
        public Sprite[] customerSprite; //�� �̹�����
        public List<Transform> inActiveCustomers; //��Ȱ��ȭ ��
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

        void createCustomer() //��Ȱ��ȭ ���� Ȱ��ȭ
        {
            if (inActiveCustomers.Count != 0) //��Ȱ��ȭ ���� ������
            {
                if (randTime == 0f) //���� �����ð� �ʱ�ȭ��
                {
                    randTime = Random.Range(randTimeMin, randTimeMax); //�����ð� ������
                }

                createTime += Time.deltaTime;

                if (createTime > randTime) //�����ð��� �Ǹ�
                {
                    randCus = Random.Range(0, inActiveCustomers.Count); //��Ȱ��ȭ ���� �ϳ� ����
                    inActiveCustomers[randCus].gameObject.SetActive(true);//���õ� �� Ȱ��ȭ
                    inActiveCustomers.Remove(inActiveCustomers[randCus]);//Ȱ��ȭ�� �� ����Ʈ ����

                    randTime = 0f; //�����ð� �ʱ�ȭ 
                    createTime = 0f;
                }
            }
        }
    }
}

