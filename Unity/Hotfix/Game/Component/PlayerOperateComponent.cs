using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    public class PlayerOperateComponent :Component
    {
        /// <summary>
        /// 父物体面板
        /// </summary>
        public GameObject Panel { get; private set; }
        /// <summary>
        /// 抢庄父面板
        /// </summary>
        public GameObject RobPanel { get; private set; }
        /// <summary>
        /// 抢庄
        /// </summary>
        public readonly List<Button> RobButtons =new List<Button>();
        /// <summary>
        /// 下注父面板
        /// </summary>
        public GameObject AddRobPanel { get; private set; }
        /// <summary>
        /// 下注
        /// </summary>
        public readonly List<Button> AddRobButtons = new List<Button>();
        /// <summary>
        /// 摊牌btn
        /// </summary>
        public Button btn_Tanpai { get; set; }
        /// <summary>
        /// 准备btn
        /// </summary>
        public Button btn_Ready { get; set; }

        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="panel"></param>
        public void SetPanel(GameObject panel)
        {
            if (Panel!=null)
            {
                return;
            }
            Panel = panel;
            ReferenceCollector rc = this.Panel.GetComponent<ReferenceCollector>();
            for (int i = 1; i <= 5; i++)
            {
                RobButtons.Add(rc.GetComponent<Button>($"Rob{i}"));
            }

            for (int i = 1; i <= 3; i++)
            {
                AddRobButtons.Add(rc.GetComponent<Button>($"AddRob{i}"));
            }

            btn_Tanpai = rc.GetComponent<Button>("btn_tanpai");
            btn_Ready = rc.GetComponent<Button>("btn_Ready");
            RobPanel = rc.Get<GameObject>("Rob");
            AddRobPanel = rc.Get<GameObject>("AddRob");


            btn_Ready.onClick.AddListener(OnReady);
        }

        /// <summary>
        /// 重置面板
        /// </summary>
        public void ResetPanel()
        {
            HideRob();
            HideAddRob();
            HideReady();
            HideTanpai();
        }

        #region Button

        private void OnReady()
        {
            try
            {
                SessionComponent.Instance.Session.Send(new Actor_GamerReady_Ntt()
                {
                    ChairId = GameTools.GetUser().ChairId
                });
                this.HideReady();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        #endregion

        public void ShowReady()
        {
            this.btn_Ready.gameObject.SetActive(true);
        }

        public void HideReady()
        {
            this.btn_Ready.gameObject.SetActive(false);
        }

        public void ShowTanpai()
        {
            this.btn_Tanpai.gameObject.SetActive(true);
        }

        public void HideTanpai()
        {
            this.btn_Tanpai.gameObject.SetActive(false);
        }

        public void ShowRob()
        {
            this.RobPanel.SetActive(true);
        }

        public void HideRob()
        {
            this.RobPanel.SetActive(false);
        }

        public void ShowAddRob()
        {
            this.AddRobPanel.SetActive(true);
        }

        public void HideAddRob()
        {
            this.AddRobPanel.SetActive(false);
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            base.Dispose();
            RobButtons.Clear();
            AddRobButtons.Clear();
            Panel.SetActive(false);
            RobPanel.SetActive(false);
            AddRobPanel.SetActive(false);
            btn_Tanpai = null;
            btn_Ready = null;
            RobPanel = null;
            AddRobPanel = null;
            Panel = null;
        }
    }
}
