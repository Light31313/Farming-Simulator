using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Config/ItemConfig", order = 1)]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private ItemTag[] tags;
    public ItemTag[] Tags => tags;
    [SerializeField] private string itemName;
    public string ItemName => itemName;

    [SerializeField] private string itemDescription;
    public string ItemDescription => itemDescription;

    [SerializeField] private Sprite spriteItem;
    public Sprite SpriteItem => spriteItem;

    [SerializeField] private int maxStack;
    public int MaxStack => maxStack;

    [SerializeField] private ItemType type;
    public ItemType Type => type;

    [SerializeField] private bool needIndicator;
    public bool NeedIndicator => needIndicator;

    [Header("For Plant")] [SerializeField] private Sprite[] spritePlantGrows;
    public Sprite[] SpritePlantGrows => spritePlantGrows;
    [SerializeField] private int[] dayToGrowEachState;
    public int[] DayToGrowEachState => dayToGrowEachState;
    [SerializeField] private long harvestPrice;
    public long HarvestPrice => harvestPrice;
    [SerializeField] private bool canHarvestMultipleTimes;
    public bool CanHarvestMultipleTimes => canHarvestMultipleTimes;
    [SerializeField] private Sprite spriteHarvestMultipleTimes;
    public Sprite SpriteHarvestMultipleTimes => spriteHarvestMultipleTimes;
    [SerializeField] private int dayToRegrow;
    public int DayToRegrow => dayToRegrow;
    
    [Header("For Weapon")] [SerializeField]
    private float attackDamage;

    public float AttackDamage => attackDamage;
}

public enum ItemType
{
    None,
    Wood,
    Milk,
    Egg,
    Hoe,
    Axe,
    WateringPot,
    CornSeed,
    BeetSeed,
    CabbageSeed,
    CarrotSeed,
    CauliflowerSeed,
    CucumberSeed,
    EggplantSeed,
    PumpkinSeed,
    RadishSeed,
    RedCabbageSeed,
    StarFruitSeed,
    TomatoSeed,
    TulipSeed,
    WheatSeed
}

public enum ItemTag
{
    Seed,
    Plant,
    Weapon,
    Tool,
    Material
}