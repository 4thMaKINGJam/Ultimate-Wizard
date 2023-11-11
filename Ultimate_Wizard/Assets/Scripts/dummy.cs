using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dummy : MonoBehaviour
{



    // Start is called before the first frame update

    //public SpriteRenderer dummyRenderer;
    [SerializeField]
    private Text text;
    private Vector2 initialPosition;  // 초기 위치 저장 변수

    
    void Start()
    {
     
        initialPosition = transform.position;
    }

    public void changeText(int candidates)
    {
        switch (candidates)
        {
            case 0:
                text.text = "더미가 소닉이 되었어!";
                break;

            case 1:
                text.text = "더미가 헤라클레스가 되었어!";
                break;

            case 2:
                text.text = "더미가 좀비가 되었어!";
                break;
        }

    }

    public void MoveToPosition(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    public void ChangeDummyImage(Sprite newSprite)
        {
        SpriteRenderer dummyRenderer = gameObject.GetComponent<SpriteRenderer>();
        dummyRenderer.sprite = newSprite;
       
    }




}
