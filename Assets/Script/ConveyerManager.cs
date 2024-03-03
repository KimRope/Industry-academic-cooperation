using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyerGame
{
    public class ConveyerManager : MonoBehaviour
    {
        public Sprite[] defaultFood;
        public List<SpriteRenderer> waitingFoodSprite; //대기 음식
        public List<int> waitingFoodNum; //대기 음식 번호

        public SpriteRenderer mainFoodSprite; //메인테이블 음식 이미지컴포넌트
        public int mainFoodNum; //메인테이블 음식 번호

        int rand;
        // Start is called before the first frame update
        void Start()
        {

            for (int i = 0; i < waitingFoodSprite.Count; i++)
            {
                rand = Random.Range(0, defaultFood.Length); //기본음식 순서 랜덤
                waitingFoodSprite[i].sprite = defaultFood[rand]; //컨베이어에 음식 이미지 변경
                waitingFoodNum.Add(rand); //컨베이어에 음식번호 배열
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mainFoodSprite.sprite == null) //메인음식이 비어있으면
            {
                ConveyerNextFood();
            }
        }
        void ConveyerNextFood() //메인음식을 변경하는 함수
        {
            //메인 음식에 이미지와 번호를 넣는다
            mainFoodSprite.sprite = waitingFoodSprite[0].sprite;
            mainFoodNum = waitingFoodNum[0];

            //대기 음식 0번의 번호를 지운다
            waitingFoodNum.Remove(waitingFoodNum[0]);

            //대기 음식의 이미지를 앞으로 당긴다
            for (int i = 0; i < waitingFoodSprite.Count - 1; i++)
            {
                waitingFoodSprite[i].sprite = waitingFoodSprite[i + 1].sprite;
            }

            //대기 음식의 마지막 번호의 이미지와 번호를 랜덤으로 생성한다
            rand = Random.Range(0, defaultFood.Length); //랜덤
            waitingFoodSprite[waitingFoodSprite.Count - 1].sprite = defaultFood[rand];
            waitingFoodNum.Add(rand);
        }
    }
}
