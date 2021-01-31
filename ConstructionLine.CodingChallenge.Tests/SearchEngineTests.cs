namespace ConstructionLine.CodingChallenge.Tests
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {

        [Test]
        public void SmallRedShirtMatches()
        {
            var shirts = new List<Shirt>
                             {
                                 new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                                 new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                                 new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                             };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void EmptySizeOptionTest()
        {
            var shirts = new List<Shirt>
                             {
                                 new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                                 new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                                 new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                                 new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                                 new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                                 new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                             };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void EmptyColorOptionTest()
        {
            var shirts = new List<Shirt>
                             {
                                 new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                                 new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                                 new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                                 new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                                 new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                                 new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                             };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }
    }
}
