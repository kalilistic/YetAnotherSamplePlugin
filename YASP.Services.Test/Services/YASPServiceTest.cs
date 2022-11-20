using System.Collections.Generic;
using Xunit;
using YASP.Models;

namespace YASP.Services.Test
{
    public class YASPServiceTest
    {
        [Fact]
        public void GetWorldsReturnsMatchingCount()
        {
            var yaspService = new YASPService(GetWorlds, GetPlayerName, GetFriendListAddOnData);
            Assert.Equal(GetWorlds().Count, yaspService.GetWorlds().Count);
        }

        [Fact]
        public void GetLocalPlayerNameReturnsName()
        {
            var yaspService = new YASPService(GetWorlds, GetPlayerName, GetFriendListAddOnData);
            Assert.Equal(GetPlayerName(), yaspService.GetLocalPlayerName.Invoke());
        }

        [Fact]
        public void GetFriendListAddOnDataReturnsFriendCount()
        {
            var yaspService = new YASPService(GetWorlds, GetPlayerName, GetFriendListAddOnData);
            Assert.Equal(GetFriendListAddOnData().FriendCount, yaspService.GetFriendListAddOnData.Invoke()?.FriendCount);
        }

        [Fact]
        public void BuildFriendsFromAddOnParsesFriendCount()
        {
            var friends = YASPService.BuildFriendsFromAddOn(GetFriendListAddOnData());
            Assert.Equal(3u, friends.FriendCount);
        }

        [Theory]
        [InlineData(1, "Siren")]
        [InlineData(3, "")]
        public void GetWorldNameById(uint id, string name)
        {
            var yaspService = new YASPService(GetWorlds, GetPlayerName, GetFriendListAddOnData);
            Assert.Equal(name, yaspService.GetWorldNameById(id));
        }

        [Fact]
        public void GetFriendsShouldHandleNull()
        {
            var yaspService = new YASPService(GetWorlds, GetPlayerName, () => null);
            Assert.Equal(0u, yaspService.GetFriends().FriendCount);
        }

        [Fact]
        public void GetFriendsShouldMapFromAddOnData()
        {
            var yaspService = new YASPService(GetWorlds, GetPlayerName, GetFriendListAddOnData);
            Assert.Equal(3u, yaspService.GetFriends().FriendCount);
        }

        [Fact]
        public void GetPlayerNameShouldReturnString()
        {
            var yaspService = new YASPService(GetWorlds, GetPlayerName, GetFriendListAddOnData);
            Assert.Equal(GetPlayerName(), yaspService.GetPlayerName());
        }

        private static FriendListAddOnData GetFriendListAddOnData()
        {
            return new FriendListAddOnData
            {
                FriendCount = "3/200",
            };
        }

        private static Dictionary<uint, string> GetWorlds()
        {
            return new Dictionary<uint, string>
            {
                { 1, "Siren" },
                { 2, "Gilgamesh" },
            };
        }

        private static string GetPlayerName()
        {
            return "Pokemon Trainer";
        }
    }
}
