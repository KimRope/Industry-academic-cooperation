using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ConveyerGame
{
    public class ClickManager : MonoBehaviour
    {
        public FollowingFood mainFoodFollowing;

        void Update()
        {
            ScreenClick();
        }

        void ScreenClick()
        {
            if (Input.GetMouseButtonDown(0)) //마우스 클릭시
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 클릭 좌표값
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

                mainFoodFollowing.isClick = true; //오브젝트 마우스 따라가기 true

            }
        }
    }
}
