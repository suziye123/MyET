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
    public class GamerUIComponent :Component
    {
        /// <summary>
        /// UI面板
        /// </summary>
        public GameObject Panel { get; private set; }
        /// <summary>
        /// 玩家昵称
        /// </summary>
        public string NickName => txt_Name.name;
        /// <summary>
        /// 头像
        /// </summary>
        public Image img_Head { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public Text txt_Name { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public Text txt_Score { get; set; }
        /// <summary>
        /// 抢庄
        /// </summary>
        public Image img_Rob { get; set; }
        /// <summary>
        /// 金币面板
        /// </summary>
        public GameObject obj_GoldBg { get; set; }
        /// <summary>
        /// 庄
        /// </summary>
        public Image img_Zhuang { get; set; }
        /// <summary>
        /// 下注分数
        /// </summary>
        public Text txt_XiaZhu { get; set; }

        /// <summary>
        /// 更新视图信息
        /// </summary>
        public void UpdateView()
        {
            this.txt_Score.text = this.GetParent<Gamer>().Money.ToString();
            this.txt_Name.text = this.GetParent<Gamer>().NickName;
        }
        /// <summary>
        /// 更新分数
        /// </summary>
        /// <param name="Score"></param>
        public void UpdateScore(int Score)
        {
            this.txt_Score.text = $"{Score}";
        }
        /// <summary>
        /// 显示金币面板
        /// </summary>
        /// <param name="Gold"></param>
        public void ShowGoldBg(int Gold)
        {
            obj_GoldBg.SetActive(true);
            txt_XiaZhu.text = $"{Gold}";
        }
        /// <summary>
        /// 显示庄
        /// </summary>
        public void ShowBanker()
        {
            this.img_Zhuang.gameObject.SetActive(true);
        }
        /// <summary>
        /// 隐藏庄
        /// </summary>
        public void HideBanker()
        {
            this.img_Zhuang.gameObject.SetActive(false);
        }

        /// <summary>
        /// 显示抢庄
        /// </summary>
        /// <param name="BankerNumber"></param>
        public void ShowRobBanker(byte BankerNumber)
        {
            //显示抢庄
            img_Rob.gameObject.SetActive(true);
            if (BankerNumber==0)
            {
                img_Rob.sprite = AltasHelp.GetFontAltas().Get<Sprite>($"W_buqiang");
            }
            else
            {
                img_Rob.sprite = AltasHelp.GetFontAltas().Get<Sprite>($"W_qiang_{BankerNumber}");
            }
        }
        /// <summary>
        /// 隐藏抢庄显示
        /// </summary>
        public void HideRobBanker()
        {
            img_Rob.gameObject.SetActive(false);
        }

        /// <summary>
        /// 隐藏金币面板
        /// </summary>
        /// <param name="Gold"></param>
        public void HideGoldBg()
        {
            obj_GoldBg.SetActive(false);
        }

        /// <summary>
        /// 重置面板
        /// </summary>
        public void ResetPanel()
        {
            img_Zhuang.gameObject.SetActive(false);
            obj_GoldBg.SetActive(false);
            img_Rob.gameObject.SetActive(false);
        }

        /// <summary>
        /// 设置面板属性
        /// </summary>
        /// <param name="panel"></param>
        public void SetPanel(GameObject panel)
        {
            if (Panel != null)
            {
                return;
            }
            this.Panel = panel;
            ReferenceCollector rc = panel.GetComponent<ReferenceCollector>();
            img_Head = rc.GetComponent<Image>($"img_Head");
            img_Rob = rc.GetComponent<Image>($"img_Rob");
            img_Zhuang = rc.GetComponent<Image>($"img_zhuang");
            txt_Name = rc.GetComponent<Text>($"txt_Name");
            txt_Score = rc.GetComponent<Text>($"txt_Score");
            txt_XiaZhu = rc.GetComponent<Text>($"txt_xiazhu");
            obj_GoldBg = rc.Get<GameObject>($"bg_Gold");
            UpdateView();
        }






        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            base.Dispose();
            Panel.gameObject.SetActive(false);
            img_Head = null;
            img_Rob = null;
            img_Zhuang = null;
            txt_Name = null;
            txt_Score = null;
            txt_XiaZhu = null;
            obj_GoldBg = null;
            Panel = null;

        }
    }
}
