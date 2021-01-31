namespace ConstructionLine.CodingChallenge
{
    using System.Collections.Generic;
    using System.Linq;

    public class SearchEngine
    {
        private readonly Shirt[] _shirts;

        private readonly SizeCount[] _sizeCounts;

        private readonly ColorCount[] _colorCounts;
        
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts.ToArray();
            _sizeCounts = Size.All.Select(x => new SizeCount { Count = 0, Size = x }).ToArray();
            _colorCounts = Color.All.Select(x => new ColorCount { Count = 0, Color = x }).ToArray();
        }


        public SearchResults Search(SearchOptions options)
        {
            var matches = this._shirts
                .Where(x => (!options.Sizes.Any() || options.Sizes.Select(s => s.Id).Contains(x.Size.Id))
                            && (!options.Colors.Any() || options.Colors.Select(c => c.Id).Contains(x.Color.Id)))
                .ToArray();

            foreach (var matchedSize in matches.GroupBy(x => x.Size.Id))
            {
                this._sizeCounts.First(x => x.Size.Id == matchedSize.Key).Count = matchedSize.Count();
            }

            foreach (var matchedColor in matches.GroupBy(x => x.Color.Id))
            {
                this._colorCounts.First(x => x.Color.Id == matchedColor.Key).Count = matchedColor.Count();
            }

            return new SearchResults
            {
                Shirts = matches.ToList(),
                SizeCounts = this._sizeCounts.ToList(),
                ColorCounts = this._colorCounts.ToList(),
            };
        }
    }
}