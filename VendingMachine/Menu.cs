using System.Runtime.InteropServices;

class IMenu
{
    private Machine _machine;

    private const string _user_menu =
    @"Interaction Menu
    1. List items
    2. Buy item
    3. Deposit money
    4. Return money
    5. Admin mode
    Enter a number of desired action";
    private const string _admin_menu =
    @"Admin menu
    1. List items
    2. Add item
    3. Remove item
    4. Modify item
    5. Take income
    6. Return to user mode
    Enter a number of desired action";

    public IMenu(Machine machine)
    {
        _machine = machine;
    }

    public void ListItems()
    {
        int i = 0;
        foreach (var item in _machine.items)
        {
            Console.WriteLine($"{i}. {item.Name}, Cost: {item.Cost}, Amount: {item.Count}");
            i++;
        }
    }

    public void Buy()
    {
        Console.WriteLine("Enter ID of item you want to buy: ");
        _machine.Buy(Convert.ToInt32(Console.ReadLine()));
    }

    public void AddItem()
    {
        Console.WriteLine("Enter a name for an item: ");
        string? name = Console.ReadLine();
        if (name == null)
        {
            Console.WriteLine("You didn't enter a name");
            return;
        }
        Console.WriteLine("Enter a cost for an item");
        decimal cost = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("Enter amount of an item");
        uint count = Convert.ToUInt32(Console.ReadLine());
        _machine.AddItem(new Item { Name = name, Cost = cost, Count = count });
    }

    public void ModifyItem()
    {
        Console.WriteLine("Enter ID of an item you want modify: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter new cost of an item (leave blank if you won't modify): ");
        decimal? new_cost = null;
        uint? new_count = null;
        string? tmp = Console.ReadLine();
        if (tmp != "")
        {
            new_cost = Convert.ToDecimal(tmp);
        }
        Console.WriteLine("Enter new amount of an item (leave blank if you won't modify): ");
        tmp = Console.ReadLine();
        if (tmp != "")
        {
            new_count = Convert.ToUInt32(tmp);
        }
        _machine.ModifyItem(id, new_count, new_cost);
    }

    public void RemoveItem()
    {
        Console.WriteLine("Enter ID of an item you want modify: ");
        int id = Convert.ToInt32(Console.ReadLine());
        _machine.RemoveItem(id);
    }

    public void DepositMoney()
    {
        HashSet<decimal> allowed_coins = [0.01m, 0.05m, 0.1m, 0.5m, 1m, 2m, 5m, 10m];
        Console.WriteLine(
        @"Allowed coins for deposit:
        1 Рубль, 2 Рубля, 5 Рублей, 10 Рублей
        1 Копейка, 5 Копеек, 10 Копеек, 50 Копеек
        Coin of what face value type you want to deposit?
        1. Рубли
        2. Копейки
        Enter the number: ");
        decimal deposited_money;
        switch (Convert.ToInt32(Console.ReadLine()))
        {
            case 1:
                Console.WriteLine("Enter what type you want to deposit: ");
                deposited_money = Convert.ToDecimal(Console.ReadLine());
                if (!allowed_coins.Contains(deposited_money))
                {
                    Console.WriteLine("This face value isn't avalible to use");
                    return;
                }
                break;

            case 2:
                Console.WriteLine("No such variant exist");
                deposited_money = Convert.ToDecimal(Console.ReadLine()) / 100;
                if (!allowed_coins.Contains(deposited_money))
                {
                    Console.WriteLine("This face value isn't avalible to use");
                    return;
                }
                break;

            default:
                Console.WriteLine("This option doesn't exist");
                return;
        }
        Console.WriteLine("How many coins you want to deposit?: ");
        _machine.DepositMoney(deposited_money * Convert.ToInt32(Console.ReadLine()));
    }

    public decimal ReturnMoney()
    {
        return _machine.ReturnMoney();
    }

    private decimal TakeIncome()
    {
        return _machine.TakeIncome();
    }

    // это можно сделать гораздо лучше, но я че то не хоч)
    public void Start()
    {
        while (true)
        {
            Console.WriteLine(_user_menu);
            switch (Convert.ToInt32(Console.ReadLine())) //надо быдо через enum но мне лень
            {
                case 1:
                    ListItems();
                    break;
                case 2:
                    Buy();
                    break;
                case 3:
                    DepositMoney();
                    break;
                case 4:
                    ReturnMoney();
                    break;
                case 5:
                    Console.WriteLine(_admin_menu);
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            ListItems();
                            break;
                        case 2:
                            AddItem();
                            break;
                        case 3:
                            RemoveItem();
                            break;
                        case 4:
                            ModifyItem();
                            break;
                        case 5:
                            TakeIncome();
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("No such variant exist");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("No such variant exist");
                    break;
            }
        }
    }
}