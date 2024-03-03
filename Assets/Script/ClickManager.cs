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
            if (Input.GetMouseButtonDown(0)) //���콺 Ŭ����
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���콺 Ŭ�� ��ǥ��
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

                mainFoodFollowing.isClick = true; //������Ʈ ���콺 ���󰡱� true

            }
        }
    }
}
