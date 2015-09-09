using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace VSBackGround
{
    #region Adornment Factory
    /// <summary>
    /// Establishes an <see cref="IAdornmentLayer"/> to place the adornment on and exports the <see cref="IWpfTextViewCreationListener"/>
    /// that instantiates the adornment on the event of a <see cref="IWpfTextView"/>'s creation
    /// </summary>
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class PurpleBoxAdornmentFactory : IWpfTextViewCreationListener
    {
        [Import]
        internal SVsServiceProvider ServiceProvider = null;   //<-- 通过这句代码，就可以获取 Visual Studio 的产物
        /// <summary>
        /// Defines the adornment layer for the scarlet adornment. This layer is ordered 
        /// after the selection layer in the Z-order
        /// </summary>
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("VSBackGround")]
        [Order(After = PredefinedAdornmentLayers.Caret)]
        public AdornmentLayerDefinition editorAdornmentLayer = null;

        /// <summary>
        /// Instantiates a VSBackGround manager when a textView is created.
        /// </summary>
        /// <param name="textView">The <see cref="IWpfTextView"/> upon which the adornment should be placed</param>
        public void TextViewCreated(IWpfTextView textView)
        {
            DTE dte = (DTE)ServiceProvider.GetService(typeof(DTE)); //<-- 获取DTE对象

            new VSBackGround(textView, dte);
        }
    }
    #endregion //Adornment Factory
}
