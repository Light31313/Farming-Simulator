using System;

public class InventoryItemData
{
    public readonly ItemConfig Config;
    public int CurrentStack { get; private set; }
    public int Slot { get; private set; }

    public InventoryItemData(ItemConfig config, int slot, int currentStack = 0)
    {
        Config = config;
        Slot = slot;
        CurrentStack = currentStack;
    }

    public void UseItem(Action onOutOfStack)
    {
        CurrentStack--;
        if (CurrentStack <= 0) onOutOfStack.Invoke();
    }

    public int AddStack(int stack)
    {
        if (CurrentStack + stack > Config.MaxStack)
        {
            var res = stack - (Config.MaxStack - CurrentStack);
            CurrentStack = Config.MaxStack;
            return res;
        }

        CurrentStack += stack;
        return 0;
    }
}