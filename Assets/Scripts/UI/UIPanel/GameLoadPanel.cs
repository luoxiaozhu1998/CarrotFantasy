namespace UI.UIPanel
{
    public class GameLoadPanel : BasePanel
    {
        /// <summary>
        /// 需要显示的时候显示
        /// </summary>
        public override void EnterPanel()
        {
            base.EnterPanel();
            gameObject.SetActive(true);
            transform.SetSiblingIndex(8);
        }
        /// <summary>
        /// 刚开始不显示
        /// </summary>
        public override void InitPanel()
        {
            base.InitPanel();
            gameObject.SetActive(false);
        }
    }
}