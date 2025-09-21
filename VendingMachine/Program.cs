class Program
{
    public static void Main()
    {
        Machine machine = new();
        IMenu menu = new(machine);
        menu.Start();
    }
}