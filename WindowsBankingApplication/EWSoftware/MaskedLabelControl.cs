using System.Drawing;
using System.Windows.Forms;

namespace EWSoftware
{
    internal class MaskedLabelControl
    {
        internal class MaskedLabel
        {
            public MaskedLabel()
            {
            }

            public BorderStyle BorderStyle { get; internal set; }
            public object DataBindings { get; internal set; }
            public Point Location { get; internal set; }
        }
    }
}