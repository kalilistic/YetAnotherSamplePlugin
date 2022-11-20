using System;
using System.Collections.Generic;
using System.Reflection;
using Dalamud.DrunkenToad.Core;
using Dalamud.DrunkenToad.Extension;
using Dalamud.DrunkenToad.ImGui;
using Dalamud.DrunkenToad.Util;
using Dalamud.Game;
using Dalamud.Logging;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Component.GUI;
using YASP.Models;
using YASP.Services;
using YASP.UserInterface;
using World = Lumina.Excel.GeneratedSheets.World;

namespace YASP.Plugin;

public class Plugin : IDalamudPlugin
{
    private readonly Dictionary<uint, string> worlds = new();
    private readonly YASPService yaspService = null!;
    private readonly WindowEx mainWindow = null!;
    private readonly WindowEx configWindow = null!;
    private string localPlayerName = string.Empty;

    public Plugin(DalamudPluginInterface pluginInterface)
    {
        // initialize dalamud and custom services
        if (!DalamudContext.Initialize(pluginInterface))
        {
            // stop initiation if failure
            PluginLog.LogError("Failed to initialize dalamud context.");
            return;
        }

        // load languages from embedded resources
        DalamudContext.Localization.LoadLanguagesFromAssembly("YASP.Plugin.Resource.Loc");

        // load data from lumina
        var worldSheet = DalamudContext.GameData.Excel.GetSheet<World>()!;
        foreach (var world in worldSheet)
        {
            this.worlds.Add(world.RowId, world.Name);
        }

        // load services
        this.yaspService = new YASPService(
            () => this.worlds,
            () => this.localPlayerName,
            GetFriendListAddOnData);

        // create main window with callbacks
        this.mainWindow = new MainWindow($"{this.Name} v{Assembly.GetExecutingAssembly().Version()}")
        {
            IsOpen = true,
            GetWorldName = id => this.yaspService.GetWorldNameById(id),
            GetFriends = () => this.yaspService.GetFriends(),
            GetPlayerName = () => this.yaspService.GetPlayerName(),
        };

        // create config window
        this.configWindow = new ConfigWindow($"{this.Name} Config")
        {
            IsOpen = true,
        };

        // add windows and enable
        DalamudContext.WindowManager.AddWindows(this.mainWindow, this.configWindow);
        DalamudContext.WindowManager.Enable();

        // register commands
        DalamudContext.Commands.Register("/yasp", "Show/hide yasp.", this.mainWindow.Toggle);
        DalamudContext.Commands.Register("/yasp-config", "Show/hide yasp config.", this.configWindow.Toggle);

        // register events
        DalamudContext.Framework.Update += this.OnFrameworkUpdate;
    }

    public string Name => "YASP";

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        try
        {
            DalamudContext.Framework.Update -= this.OnFrameworkUpdate;
            DalamudContext.Dispose();
        }
        catch (Exception)
        {
            PluginLog.LogError("Failed to dispose properly.");
        }
    }

    private static unsafe FriendListAddOnData? GetFriendListAddOnData()
    {
        var unitBase = (AtkUnitBase*)DalamudContext.GameGui.GetAddonByName("FriendList", 1);
        if (!AddOnUtil.IsUnitBaseReady(unitBase)) return null;
        return new FriendListAddOnData
        {
            FriendCount = AddOnUtil.GetNodeText(unitBase, 19),
        };
    }

    private void OnFrameworkUpdate(Framework framework)
    {
        this.localPlayerName = DalamudContext.PluginInterface.Sanitize(
            DalamudContext.ClientState.LocalPlayer?.Name.TextValue ?? string.Empty);
    }
}
