using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            for (int i = 0; i < this.Pukes.Count; i++)
            {
                this.Pukes[i].sprite = GameTools.GetTexSprite(cards[i]);
            }
        }

        /// <summary>
        /// 隐藏所有牌
        /// </summary>
        public void HideAllPuke()
        {
            for (int i = 0; i < this.Pukes.Count; i++)
            {
                this.Pukes[i].sprite = GameTools.GetBackPuke();
            }
        }

        public void ShowNiuShu()
        {
            img_Niu.gameObject.SetActive(true);
            //牛型判断
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

            Panel.gameObject.SetActive(false);
            this.Panel = null;
            Pukes.Clear();
            img_Niu = null;
        }
    }
}
