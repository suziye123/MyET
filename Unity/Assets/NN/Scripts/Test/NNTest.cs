using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNTest : MonoBehaviour {

    [SerializeField]
    public byte[] Cards = new byte[]{13,12,1,5,4};

	// Use this for initialization
	void Start ()
	{
	    jisuan(this.Cards);

	}

    public byte GetValue(byte Value)
    {
        byte temp = Value;
        if (temp > 10)
        {
            temp = 10;
        }
        return temp;
    }

    public void jisuan(byte[] Cards)
    {
        bool IsHaveNiu = false;
        int RemainNumber = 0;
        int NiuNumber = -1;
        for (int i = 0; i <= 2; i++)
        {
            for (int j = i+1; j <= 3; j++)
            {
                for (int k = j+1; k <= 4; k++)
                {
                    int NumberA = GetValue(Cards[i]);
                    int NumberB = GetValue(Cards[j]);
                    int NumberC = GetValue(Cards[k]);
                    int Numbers = NumberA + NumberB + NumberC;
                    if (Numbers%10==0)
                    {
                        IsHaveNiu = true;

                        for (int l = 0; l <= 4; l++)
                        {
                            if (l!=i&&l!=j&&l!=k)
                            {
                                RemainNumber += GetValue(Cards[l]);
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
