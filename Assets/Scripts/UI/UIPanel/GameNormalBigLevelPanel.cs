using System.Net.NetworkInformation;
using Manager.NormalManager;
using UI.UI;
using UnityEngine;

namespace UI.UIPanel
{
    public class GameNormalBigLevelPanel : BasePanel
    {
        public Transform bigLevelContentTrans;
        public int bigLevelPageCount;
        private SlideScrollView m_SlideScrollView;
        private PlayerManager m_PlayerManager;
        private Transform[] m_BigLevelPage;

        private bool m_HasRigisterEvent;
        
    }
}