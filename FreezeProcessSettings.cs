using System.Windows.Forms;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace FreezeProcess;

public class FreezeProcessSettings : ISettings
{
    public ToggleNode Enable { get; set; } = new ToggleNode(false);

    public HotkeyNode Freeze { get; set; } = new HotkeyNode(Keys.None);
}