using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Reach.Platform.Components.Data;

[CascadingTypeParameter(name: nameof(TItem))]
public partial class DraggableList<TItem>
    where TItem : class
{
    private class OrderedItem<TModel>
        where TModel : class
    {
        public bool IsDragOver { get; set; } = false;

        public int Order { get; set; }

        public required TModel Model { get; init; }
    }

    private readonly IJSRuntime _jsRuntime;

    private OrderedItem<TItem>? _currentDragItem = null;
    private List<OrderedItem<TItem>> _internalItems = new();

    public DraggableList(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    protected override Task OnParametersSetAsync()
    {
        _internalItems.Clear();

        var index = 0;
        foreach(var item in Items)
        {
            _internalItems.Add(new OrderedItem<TItem>
            {
                Order = index,
                Model = item
            });

            index++;
        }

        return Task.CompletedTask;
    }
    
    private async Task HandleDrop(OrderedItem<TItem> item)
    {
        if(_currentDragItem == null)
        {
            Console.WriteLine("Can't handle drop");
            return;
        }

        Console.WriteLine("Handling drop");

        int originalOrderLanding = item.Order;

        _internalItems.Where(x => x.Order >= item.Order)
            .ToList()
            .ForEach(m => m.Order++);

        _currentDragItem.Order = originalOrderLanding;

        _internalItems.ToList()
            .ForEach(m => m.IsDragOver = false);

        _internalItems = _internalItems
            .OrderBy(m => m.Order)
            .ToList();

        foreach(var model in _internalItems)
        {
            Console.WriteLine(model.Order);
        }

        Items = _internalItems
            .OrderBy(m => m.Order)
            .Select(m => m.Model);

        foreach(var i in Items)
        {
            Console.WriteLine(JsonConvert.SerializeObject(i));
        }

        if(ItemsChanged.HasDelegate)
        {
            await ItemsChanged.InvokeAsync(Items);
        }

        StateHasChanged();
    }


    [Parameter]
    public RenderFragment<TItem> ItemTemplate { get; set; } = null!;

    [Parameter]
    public Func<TItem, TItem, bool> Equivalent { get; set; } = null!;

    [Parameter]
    public IEnumerable<TItem> Items { get; set; } = [];

    [Parameter]
    public EventCallback<IEnumerable<TItem>> ItemsChanged { get; set; }
}
