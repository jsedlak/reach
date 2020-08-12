namespace Reach.Elements.ViewModel
{
    public class DropdownItem 
    { 
        public DropdownItem() { }

        public DropdownItem(string title) 
            : this(title, title) { }

        public DropdownItem(string title, object value, bool isDisabled = false)
        {
            Title = title;
            Value = value;
            IsDisabled = isDisabled;
        }

        /// <summary>
        /// Gets or Sets the title for the dropdown item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets the value of the dropdown item
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or Sets whether or not this item may be selected
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
