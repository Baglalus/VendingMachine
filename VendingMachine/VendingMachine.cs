class Item
{
    public required string Name { get; init; }
    public decimal Cost;
    public uint Count;
}
class Machine
{
    public List<Item> items { get; } = new List<Item>();
    private decimal _income;
    private decimal _current_money;


    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(int id)
    {
        if (id < 0 || id > items.Count)
        {
            Console.WriteLine($"Item with id: {id} doesn't exist");
            return;
        }

        items.RemoveAt(id);
    }

    public void ModifyItem(int id, uint? count = null, decimal? cost = null)
    {
        if (id < 0 || id > items.Count)
        {
            Console.WriteLine($"Item with id: {id} doesn't exist");
            return;
        }

        if (count.HasValue)
        {
            items[id].Count = count.Value;
        }
        if (cost.HasValue)
        {
            items[id].Cost = cost.Value;
        }
    }

    public void Buy(int id)
    {
        if (id < 0 || id > items.Count)
        {
            Console.WriteLine($"Item with id: {id} doesn't exist");
            return;
        }

        if (items[id].Count == 0)
        {
            Console.WriteLine($"This item is out of stock");
            return;
        }

        if (_current_money < items[id].Cost)
        {
            Console.WriteLine($"You don't have enought money to buy an item");
            return;
        }

        items[id].Count--;
        _income += items[id].Cost;
        _current_money -= items[id].Cost;
        ReturnMoney();
    }

    public decimal TakeIncome()
    {
        decimal tmp = _income;
        _income = 0;
        return tmp;
    }

    public decimal ReturnMoney()
    {
        decimal tmp = _current_money;
        _current_money = 0;
        return tmp;
    }

    public void DepositMoney(decimal money)
    {
        _current_money += money;
    }
}