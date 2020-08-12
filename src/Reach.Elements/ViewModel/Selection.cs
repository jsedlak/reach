using System.Collections.Generic;

namespace Reach.Elements.ViewModel
{
    public sealed class Selection<TModel>
    {
        /// <summary>
        /// Gets or Sets the items that are part of the selection
        /// </summary>
        public IEnumerable<TModel> Items { get; set; }
    }
}
