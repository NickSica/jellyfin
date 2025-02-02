using System;
using System.Collections.Generic;
using Jellyfin.Api.Helpers;
using Jellyfin.Data.Enums;
using Xunit;

namespace Jellyfin.Api.Tests.Helpers
{
    public static class RequestHelpersTests
    {
        [Theory]
        [MemberData(nameof(GetOrderBy_Success_TestData))]
        public static void GetOrderBy_Success(IReadOnlyList<string> sortBy, IReadOnlyList<SortOrder> requestedSortOrder, (string, SortOrder)[] expected)
        {
            Assert.Equal(expected, RequestHelpers.GetOrderBy(sortBy, requestedSortOrder));
        }

        public static TheoryData<IReadOnlyList<string>, IReadOnlyList<SortOrder>, (string, SortOrder)[]> GetOrderBy_Success_TestData()
        {
            var data = new TheoryData<IReadOnlyList<string>, IReadOnlyList<SortOrder>, (string, SortOrder)[]>();

            data.Add(
                Array.Empty<string>(),
                Array.Empty<SortOrder>(),
                Array.Empty<(string, SortOrder)>());

            data.Add(
                new string[]
                {
                    "IsFavoriteOrLiked",
                    "Random"
                },
                Array.Empty<SortOrder>(),
                new (string, SortOrder)[]
                {
                    ("IsFavoriteOrLiked", SortOrder.Ascending),
                    ("Random", SortOrder.Ascending),
                });

            data.Add(
                new string[]
                {
                    "SortName",
                    "ProductionYear"
                },
                new SortOrder[]
                {
                    SortOrder.Descending
                },
                new (string, SortOrder)[]
                {
                    ("SortName", SortOrder.Descending),
                    ("ProductionYear", SortOrder.Descending),
                });

            return data;
        }

        [Fact]
        public static void GetItemTypeStrings_Empty_Empty()
        {
            Assert.Empty(RequestHelpers.GetItemTypeStrings(Array.Empty<BaseItemKind>()));
        }

        [Fact]
        public static void GetItemTypeStrings_Valid_Success()
        {
            BaseItemKind[] input =
            {
                BaseItemKind.AggregateFolder,
                BaseItemKind.Audio,
                BaseItemKind.BasePluginFolder,
                BaseItemKind.CollectionFolder
            };

            string[] expected =
            {
                "AggregateFolder",
                "Audio",
                "BasePluginFolder",
                "CollectionFolder"
            };

            var res = RequestHelpers.GetItemTypeStrings(input);

            Assert.Equal(expected, res);
        }
    }
}
