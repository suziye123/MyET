using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    public class HandCardComponent :Component
    {
        public GameObject Panel { get; private set; }

        //手牌
        public readonly List<Image> Pukes = new List<Image>();

        public Image img_Niu { get; set; }
        /// <summary>
        /// 设置父面板
        /// </summary>
        /// <param name="panel"></param>
        public void SetPanel(GameObject panel)
        {
            if (this.Panel!=null)
            {
                return;
            }
            Panel = panel;
            Panel.SetActive(true);
            Pukes.Clear();
            ReferenceCollector rc = this.Panel.GetComponent<ReferenceCollector>();
            for (int i = 1; i <= 5; i++)
            {
                Pukes.Add(rc.GetComponent<Image>($"{i}"));
            }
            this.img_Niu = rc.GetComponent<Image>("niu");
        }

        /// <summary>
        /// 显示手牌
        /// </summary>
        /// <param name="cards"></param>
        public void ShowPuke(byte[] cards)
        {
            SetActive(true);
            for (int i = 0; i < this.Pukes.Count; i++)
            {
                this.Pukes[i].sprite = GameTools.GetTexSprite(cards[i]);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            HideNiu();
            SetActive(false);
        }



        
        /// <summary>
        /// 显示扑克
        /// </summary>
        /// <param name="IsShow"></param>
        public void SetActive(bool IsShow)
        {
            for (int i = 0; i < this.Pukes.Count; i++)
            {
                this.Pukes[i].gameObject.SetActive(IsShow);
            }
        }



        /// <summary>
        /// 显示所有背牌
        /// </summary>
        public void HideAllPuke()
        {
            SetActive(true);
            for (int i = 0; i < this.Pukes.Count; i++)
            {
                this.Pukes[i].sprite = GameTools.GetBackPuke();
            }
        }
        /// <summary>
        /// 显示牛数
        /// </summary>
        /// <param name="CardType"></param>
        public void ShowNiuShu(byte CardType)
        {
            img_Niu.gameObject.SetActive(true);
            //牛型判断
            switch (CardType)
            {
                case GameLogic.NO_NIU:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_wuniu");
                    break;
                case GameLogic.NIU_1:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niuyi");
                    break;
                case GameLogic.NIU_2:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niuer");
                    break;
                case GameLogic.NIU_3:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niusan");
                    break;
                case GameLogic.NIU_4:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niusi");
                    break;
                case GameLogic.NIU_5:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niuwu");
                    break;
                case GameLogic.NIU_6:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niuliu");
                    break;
                case GameLogic.NIU_7:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niuqi");
                    break;
                case GameLogic.NIU_8:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niuba");
                    break;
                case GameLogic.NIU_9:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niujiu");
                    break;
                case GameLogic.NIU_NIU:
                    img_Niu.sprite = AltasHelp.GetFontAltas().Get<Sprite>("W_niuniu");
                    break;
            }
        }


        public void HideNiu()
        {
            img_Niu.gameObject.SetActive(false);
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            base.Dispose();

            this.Panel = null;
            Pukes.Clear();
            img_Niu = null;
        }
    }
}
