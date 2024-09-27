using System.Text.RegularExpressions;
using Nugaeva_Alsu_OZKT_42_21.Database.Configurations;

using Nugaeva_Alsu_OZKT_42_21.Filters.StudentFilters;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Microsoft.EntityFrameworkCore;
using Group = Nugaeva_Alsu_OZKT_42_21.Database.Models.Group;

namespace Nugaeva_Alsu_OZKT_42_21.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_KT4221_True()
        {
            //arrange
            var testGroup = new Group
            {
                GroupName = "KT-42-21"
            };

            //act
            var result = testGroup.IsValidGroupName();

            //assert
            Assert.True(result);
        }
    }
}
    