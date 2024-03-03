using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyerGame
{
    public class ConveyerManager : MonoBehaviour
    {
        public Sprite[] defaultFood;
        public List<SpriteRenderer> waitingFoodSprite; //��� ����
        public List<int> waitingFoodNum; //��� ���� ��ȣ

        public SpriteRenderer mainFoodSprite; //�������̺� ���� �̹���������Ʈ
        public int mainFoodNum; //�������̺� ���� ��ȣ

        int rand;
        // Start is called before the first frame update
        void Start()
        {

            for (int i = 0; i < waitingFoodSprite.Count; i++)
            {
                rand = Random.Range(0, defaultFood.Length); //�⺻���� ���� ����
                waitingFoodSprite[i].sprite = defaultFood[rand]; //�����̾ ���� �̹��� ����
                waitingFoodNum.Add(rand); //�����̾ ���Ĺ�ȣ �迭
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mainFoodSprite.sprite == null) //���������� ���������
            {
                ConveyerNextFood();
            }
        }
        void ConveyerNextFood() //���������� �����ϴ� �Լ�
        {
            //���� ���Ŀ� �̹����� ��ȣ�� �ִ´�
            mainFoodSprite.sprite = waitingFoodSprite[0].sprite;
            mainFoodNum = waitingFoodNum[0];

            //��� ���� 0���� ��ȣ�� �����
            waitingFoodNum.Remove(waitingFoodNum[0]);

            //��� ������ �̹����� ������ ����
            for (int i = 0; i < waitingFoodSprite.Count - 1; i++)
            {
                waitingFoodSprite[i].sprite = waitingFoodSprite[i + 1].sprite;
            }

            //��� ������ ������ ��ȣ�� �̹����� ��ȣ�� �������� �����Ѵ�
            rand = Random.Range(0, defaultFood.Length); //����
            waitingFoodSprite[waitingFoodSprite.Count - 1].sprite = defaultFood[rand];
            waitingFoodNum.Add(rand);
        }
    }
}
