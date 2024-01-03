using strange.extensions.signal.impl;

public static class InventorySignals
{
    public static readonly Signal<int> OnChangeItemHold = new();
    public static readonly Signal<int> OnUpdateItem = new();
    
}