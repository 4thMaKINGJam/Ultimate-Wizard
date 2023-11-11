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
    private Vector2 initialPosition;  // �ʱ� ��ġ ���� ����

    
    void Start()
    {
     
        initialPosition = transform.position;
    }

    public void changeText(int candidates)
    {
        switch (candidates)
        {
            case 0:
                text.text = "���̰� �Ҵ��� �Ǿ���!";
                break;

            case 1:
                text.text = "���̰� ���Ŭ������ �Ǿ���!";
                break;

            case 2:
                text.text = "���̰� ���� �Ǿ���!";
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
