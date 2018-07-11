using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNTest : MonoBehaviour {

    [SerializeField]
    public byte[] Cards = new byte[]{33,12,41,54,3};

	// Use this for initialization
	void Start ()
	{
	    jisuan(this.Cards);

	}

    /// <summary>
    /// 得到卡片的值
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    private static byte GetCardValue(byte Value)
    {
        byte MASK_COLOR = 0xF0;
        byte MASK_VALUE = 0x0F;
        byte bColor = (byte)((Value & MASK_COLOR) >> 4);
        byte bValue = (byte)(Value & MASK_VALUE);
        if (bValue > 10)
        {
            bValue = 10;
        }
        return bValue;
    }

    public void jisuan(byte[] Cards)
    {
        bool IsHaveNiu = false;
        int RemainNumber = 0;
        int NiuNumber = -1;
        for (int i = 0; i <= 2; i++)
        {
            if (IsHaveNiu)
            {
                break;
            }
            for (int j = i+1; j <= 3; j++)
            {
                if (IsHaveNiu)
                {
                    break;
                }
                for (int k = j+1; k <= 4; k++)
                {
                    if (IsHaveNiu)
                    {
                        break;
                    }
                    int NumberA = GetCardValue(Cards[i]);
                    int NumberB = GetCardValue(Cards[j]);
                    int NumberC = GetCardValue(Cards[k]);
                    int Numbers = NumberA + NumberB + NumberC;
                    if (Numbers%10==0)
                    {
                        IsHaveNiu = true;

                        for (int l = 0; l <= 4; l++)
                        {
                            if (l!=i&&l!=j&&l!=k)
                            {
                                RemainNumber += GetCardValue(Cards[l]);
                            }
                        }

                        //判断牛型
                        NiuNumber = RemainNumber % 10;
                        
                        //if (NiuNumber<temp||temp==0)
                        //{
                        //    NiuNumber = temp;
                        //}
                    }
                }
            }
        }
        Debug.Log($"牛型:{NiuNumber}");
        if (!IsHaveNiu)
        {
            Debug.Log("没牛");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
