using EnvDTE;
using Microsoft.VisualStudio.Text.Editor;
using System.Windows.Controls;

namespace VSBackGround
{
    /// <summary>
    /// VSBackGroundTool.xaml 的交互逻辑
    /// </summary>
    public partial class VSBackGroundTool : UserControl
    {
        private DTE mDTE;

        public VSBackGroundTool()
        {
            InitializeComponent();
        }

        internal void InitDTE(DTE dte)
        {
            mDTE = dte;
        }


        internal void OnSizeChange(IWpfTextView _view)
        {
            this.LabelCurScript.Content = mDTE.ActiveDocument.ActiveWindow.Caption;
            int w = 5 * this.LabelCurScript.Content.ToString().Length;
            Canvas.SetRight(this, _view.ViewportRight - 60);
            Canvas.SetLeft(this, _view.ViewportRight - 60 - w);
            Canvas.SetTop(this, _view.ViewportTop + 30);
        }
    }
}