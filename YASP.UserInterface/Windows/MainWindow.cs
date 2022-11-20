using System;
using System.Numerics;

using Dalamud.DrunkenToad.ImGui;
using Dalamud.Interface.Colors;
using ImGuiNET;
using YASP.Models;

namespace YASP.UserInterface;

public class MainWindow : WindowEx
{
    public MainWindow(string name)
        : base(name)
    {
        this.Size = new Vector2(320f, 350f);
        this.SizeCondition = ImGuiCond.Always;
    }

    public Func<uint, string>? GetWorldName { get; init; }

    public Func<Friends>? GetFriends { get; init; }

    public Func<World?> GetWorld { get; init; } = null!;

    public Func<string> GetPlayerName { get; init; } = null!;

    public override void Draw()
    {
        this.ConfigExample();
        this.LuminaExample();
        this.ClientStateExample();
        this.AddOnExample();
        this.LocExample();
    }

    public void ConfigExample()
    {
        ImGui.TextColored(ImGuiColors.DalamudViolet, "Get/Set Data from Plugin Config");
        ImGui.Text($"Is Enabled: {this.Configuration.Get("isEnabled")}");
        FlexGui.Checkbox("Is Enabled", "isEnabled");
        ImGui.Dummy(new Vector2(10f));
    }

    public void LuminaExample()
    {
        ImGui.TextColored(ImGuiColors.DalamudViolet, "Get Game Data from Lumina");
        ImGui.Text($"World: {this.GetWorldName?.Invoke(60)}");
        ImGui.Dummy(new Vector2(10f));
    }

    public void ClientStateExample()
    {
        ImGui.TextColored(ImGuiColors.DalamudViolet, "Get ClientState Data from Dalamud");
        ImGui.Text($"Player Name: {this.GetPlayerName.Invoke()}");
        ImGui.Dummy(new Vector2(10f));
    }

    public void AddOnExample()
    {
        ImGui.TextColored(ImGuiColors.DalamudViolet, "Get Data from Game GUI (open friend list)");
        ImGui.Text($"Friend Count: {this.GetFriends?.Invoke().FriendCount}");
        ImGui.Dummy(new Vector2(10f));
    }

    public void LocExample()
    {
        ImGui.TextColored(ImGuiColors.DalamudViolet, "Localize String (change language to fr/ja)");
        ImGui.Text($"Localized Text: {this.Localize?.Invoke("greeting")}");
        ImGui.Dummy(new Vector2(10f));
    }
}
