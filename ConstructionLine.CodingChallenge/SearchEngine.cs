namespace ConstructionLine.CodingChallenge
{
    using System.Collections.Generic;
    using System.Linq;

    public class SearchEngine
    {
        private readonly Shirt[] _shirts;

        
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts.ToArray();
        }
        
        public SearchResults Search(SearchOptions options)
        {
            var matches = this._shirts
                .Where(x => (!options.Sizes.Any() || options.Sizes.Select(s => s.Id).Contains(x.Size.Id))
                            && (!options.Colors.Any() || options.Colors.Select(c => c.Id).Contains(x.Color.Id)))
                .ToArray();

            var sizeCounts = Size.All.Select(x => new SizeCount { Count = 0, Size = x }).ToArray();
            var colorCounts = Color.All.Select(x => new ColorCount { Count = 0, Color = x }).ToArray();

            foreach (var matchedSize in matches.GroupBy(x => x.Size.Id))
            {
                sizeCounts.First(x => x.Size.Id == matchedSize.Key).Count = matchedSize.Count();
            }

            foreach (var matchedColor in matches.GroupBy(x => x.Color.Id))
            {
                colorCounts.First(x => x.Color.Id == matchedColor.Key).Count = matchedColor.Count();
            }

            return new SearchResults
            {
                Shirts = matches.ToList(),
                SizeCounts = sizeCounts.ToList(),
                ColorCounts = colorCounts.ToList(),
            };
        }
    }
}