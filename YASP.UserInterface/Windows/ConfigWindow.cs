using System.Numerics;

using Dalamud.DrunkenToad.ImGui;
using ImGuiNET;

namespace YASP.UserInterface;

public class ConfigWindow : WindowEx
{
    public ConfigWindow(string name)
        : base(name)
    {
        this.Size = new Vector2(320f, 350f);
        this.SizeCondition = ImGuiCond.Always;
    }

    public override void Draw()
    {
        ImGui.Text("Can put some settings here.");
    }
}
