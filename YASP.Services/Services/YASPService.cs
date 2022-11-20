using System;
using System.Collections.Generic;
using YASP.Models;

namespace YASP.Services;

public class YASPService
{
    private readonly Dictionary<uint, string> worlds;
    private readonly Friends friends = new();

    public YASPService(
        Func<Dictionary<uint, string>> getWorlds,
        Func<string> getLocalPlayerName,
        Func<FriendListAddOnData?> getFriendListAddOnData)
    {
        this.GetWorlds = getWorlds;
        this.GetLocalPlayerName = getLocalPlayerName;
        this.GetFriendListAddOnData = getFriendListAddOnData;
        this.worlds = this.GetWorlds.Invoke();
    }

    public Func<Dictionary<uint, string>> GetWorlds { get; init; }
    public Func<string> GetLocalPlayerName { get; init; }
    public Func<FriendListAddOnData?> GetFriendListAddOnData { get; init; }

    public static Friends BuildFriendsFromAddOn(FriendListAddOnData data)
    {
        return new Friends
        {
            FriendCount = Convert.ToUInt32(data.FriendCount.Split("/", StringSplitOptions.TrimEntries)[0]),
        };
    }

    public string GetWorldNameById(uint id)
    {
        foreach (var world in this.worlds)
        {
            if (world.Key == id) return world.Value;
        }

        return string.Empty;
    }

    public Friends GetFriends()
    {
        var data = this.GetFriendListAddOnData.Invoke();
        if (data == null) return this.friends;
        return BuildFriendsFromAddOn(data);
    }

    public string GetPlayerName()
    {
        return this.GetLocalPlayerName.Invoke();
    }
}
