using System;
using NoteAppBackend.DomainModels;
using NoteAppBackend.DomainModels.DataTransferObjects;

namespace NoteAppBackend.Persistence.DataGenerators;

public class NoteTypeGenerator
{
    // NoteType name format:
    // <Adjectives> <Noun>[<Type> <HexColorCode>]
    //
    //Examples:
    // Tepid Hostel Jottings #123456
    // Cold Pilkington Discussions

    private string[] Adjectives =
    [
        "Amazing", "Beautiful", "Brilliant", "Charming", "Delightful", "Elegant", "Fantastic", "Gorgeous", "Incredible", "Joyful",
        "Kind", "Lovely", "Magnificent", "Nice", "Outstanding", "Perfect", "Quaint", "Radiant", "Stunning", "Terrific",
        "Unique", "Vibrant", "Wonderful", "Excellent", "Fabulous", "Graceful", "Happy", "Inspiring", "Jovial", "Keen",
        "Luminous", "Marvelous", "Noble", "Optimistic", "Peaceful", "Quality", "Remarkable", "Splendid", "Tranquil", "Uplifting",
        "Valuable", "Wise", "Youthful", "Adventurous", "Bold", "Creative", "Daring", "Energetic", "Fearless", "Generous",
        "Honest", "Intelligent", "Jubilant", "Knowledgeable", "Loyal", "Motivated", "Nurturing", "Open", "Passionate", "Quick",
        "Resilient", "Strong", "Thoughtful", "Understanding", "Versatile", "Warm", "Xenial", "Yearning", "Zealous", "Active",
        "Bright", "Calm", "Dedicated", "Enthusiastic", "Friendly", "Gentle", "Helpful", "Innovative", "Joyous", "Kindhearted",
        "Lively", "Mindful", "Natural", "Organized", "Patient", "Quiet", "Reliable", "Sincere", "Trustworthy", "Upbeat",
        "Vigorous", "Warmhearted", "Xenodochial", "Youthful", "Zealous", "Ambitious", "Brave", "Confident", "Determined", "Empathetic",
        "Focused", "Grateful", "Hopeful", "Imaginative", "Jovial", "Keen", "Loving", "Modest", "Nurturing", "Optimistic",
        "Persistent", "Quirky", "Respectful", "Sincere", "Tenacious", "Understanding", "Vivacious", "Wise", "Xenial", "Youthful",
        "Zealous", "Adaptable", "Benevolent", "Compassionate", "Diligent", "Empathetic", "Forgiving", "Genuine", "Humble", "Inspiring",
        "Judicious", "Kind", "Loyal", "Merciful", "Noble", "Openhearted", "Patient", "Quiet", "Resilient", "Sincere",
        "Tolerant", "Understanding", "Virtuous", "Wise", "Xenial", "Youthful", "Zealous", "Affectionate", "Benevolent", "Caring",
        "Devoted", "Empathetic", "Faithful", "Gentle", "Honest", "Inspiring", "Just", "Kind", "Loving", "Merciful",
        "Nurturing", "Optimistic", "Patient", "Quiet", "Respectful", "Sincere", "Tender", "Understanding", "Virtuous", "Warm",
        "Xenial", "Youthful", "Zealous", "Adventurous", "Bold", "Courageous", "Daring", "Energetic", "Fearless", "Gallant",
        "Heroic", "Intrepid", "Jovial", "Keen", "Lively", "Mighty", "Noble", "Outgoing", "Passionate", "Quick",
        "Resilient", "Strong", "Tenacious", "Unstoppable", "Vigorous", "Willing", "Xenial", "Youthful", "Zealous", "Amazing"
    ];

    private string[] Nouns =
    [
        "Adventure", "Beauty", "Courage", "Dream", "Energy", "Freedom", "Growth", "Hope", "Inspiration", "Joy",
        "Knowledge", "Love", "Magic", "Nature", "Opportunity", "Peace", "Quest", "Romance", "Success", "Truth",
        "Unity", "Victory", "Wisdom", "Youth", "Achievement", "Belief", "Challenge", "Discovery", "Experience", "Future",
        "Goal", "Happiness", "Innovation", "Journey", "Kindness", "Leadership", "Motivation", "Nurture", "Optimism", "Passion",
        "Quality", "Resilience", "Strength", "Talent", "Understanding", "Value", "Wonder", "Excellence", "Faith", "Grace",
        "Harmony", "Integrity", "Justice", "Kindness", "Liberty", "Mercy", "Nobility", "Openness", "Patience", "Respect",
        "Serenity", "Trust", "Unity", "Virtue", "Warmth", "Xeniality", "Yearning", "Zeal", "Ambition", "Bravery",
        "Creativity", "Determination", "Empathy", "Fortitude", "Generosity", "Honesty", "Innovation", "Justice", "Kindness", "Loyalty",
        "Modesty", "Nobility", "Optimism", "Patience", "Quality", "Respect", "Sincerity", "Tolerance", "Understanding", "Virtue",
        "Wisdom", "Xeniality", "Youth", "Zeal", "Art", "Book", "City", "Dance", "Earth", "Fire",
        "Garden", "House", "Island", "Journey", "Kingdom", "Lake", "Mountain", "Ocean", "Palace", "River",
        "Sky", "Tree", "Valley", "Water", "Xenon", "Yacht", "Zoo", "Apple", "Banana", "Cherry",
        "Date", "Elderberry", "Fig", "Grape", "Honeydew", "Kiwi", "Lemon", "Mango", "Orange", "Papaya",
        "Quince", "Raspberry", "Strawberry", "Tangerine", "Ugli", "Vanilla", "Watermelon", "Xigua", "Yam", "Zucchini",
        "Airplane", "Boat", "Car", "Dragon", "Elephant", "Falcon", "Giraffe", "Horse", "Iguana", "Jaguar",
        "Kangaroo", "Lion", "Monkey", "Nightingale", "Owl", "Penguin", "Quail", "Rabbit", "Snake", "Tiger",
        "Unicorn", "Vulture", "Whale", "Xenops", "Yak", "Zebra", "Actor", "Baker", "Chef", "Doctor",
        "Engineer", "Farmer", "Guard", "Hunter", "Inventor", "Judge", "Knight", "Lawyer", "Musician", "Nurse",
        "Officer", "Pilot", "Queen", "Ranger", "Scientist", "Teacher", "Undertaker", "Veterinarian", "Writer", "Xenologist",
        "Yoga", "Zoologist", "Angel", "Bishop", "Cardinal", "Deacon", "Elder", "Father", "Guardian", "Healer",
        "Innkeeper", "Judge", "Keeper", "Leader", "Master", "Noble", "Oracle", "Priest", "Queen", "Ruler",
        "Saint", "Teacher", "Unicorn", "Virtue", "Wizard", "Xenial", "Youth", "Zealot", "Academy", "Bank",
        "Castle", "Dungeon", "Empire", "Fortress", "Guild", "Hospital", "Institute", "Jail", "Kingdom", "Library",
        "Monastery", "Nation", "Office", "Palace", "Quarry", "Republic", "School", "Temple", "University", "Village",
        "Workshop", "Xenodochium", "Yard", "Zoo", "Amulet", "Book", "Crown", "Diamond", "Emerald", "Feather",
        "Gem", "Helmet", "Icon", "Jewel", "Key", "Locket", "Medal", "Necklace", "Orb", "Pearl",
        "Quill", "Ring", "Staff", "Tome", "Urn", "Vial", "Wand", "Xylophone", "Yarn", "Zither",
        "Arrow", "Bow", "Crossbow", "Dagger", "Epee", "Falchion", "Glaive", "Halberd", "Iron", "Javelin",
        "Knife", "Lance", "Mace", "Nunchaku", "Oar", "Pike", "Quarterstaff", "Rapier", "Sword", "Trident",
        "Unicorn", "Voulge", "Whip", "Xiphos", "Yari", "Zweihander", "Alchemy", "Brewing", "Cooking", "Divination",
        "Enchanting", "Farming", "Gardening", "Healing", "Inscription", "Jewelry", "Knitting", "Leatherworking", "Mining", "Navigation",
        "Oratory", "Pottery", "Quilting", "Riding", "Sailing", "Tailoring", "Underwater", "Ventriloquism", "Weaving", "Xenology",
        "Yoga", "Zoology", "Arena", "Battlefield", "Colosseum", "Dueling", "Expedition", "Fight", "Gymnasium", "Hunt",
        "Invasion", "Journey", "Knight", "League", "Match", "Naval", "Olympics", "Pilgrimage", "Quest", "Race",
        "Siege", "Tournament", "Underwater", "Voyage", "War", "Xenial", "Yacht", "Zoo", "Alchemy", "Biology",
        "Chemistry", "Dynamics", "Economics", "Finance", "Geography", "History", "Informatics", "Journalism", "Kinetics", "Linguistics",
        "Mathematics", "Neuroscience", "Oceanography", "Physics", "Quantum", "Robotics", "Sociology", "Technology", "Urban", "Virology",
        "Welfare", "Xenology", "Yoga", "Zoology", "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig",
        "Grape", "Honeydew", "Kiwi", "Lemon", "Mango", "Orange", "Papaya", "Quince", "Raspberry", "Strawberry",
        "Tangerine", "Ugli", "Vanilla", "Watermelon", "Xigua", "Yam", "Zucchini", "Acorn", "Bark", "Cedar",
        "Dogwood", "Elm", "Fir", "Ginkgo", "Hickory", "Ivy", "Juniper", "Koa", "Larch", "Maple",
        "Nectarine", "Oak", "Pine", "Quince", "Redwood", "Spruce", "Tulip", "Umbrella", "Vine", "Willow",
        "Xerophyte", "Yew", "Zebrawood", "Amber", "Bronze", "Copper", "Diamond", "Emerald", "Feldspar", "Gold",
        "Hematite", "Iron", "Jade", "Kyanite", "Lapis", "Malachite", "Nickel", "Opal", "Pearl", "Quartz",
        "Ruby", "Silver", "Topaz", "Uranium", "Vanadium", "Wulfenite", "Xenotime", "Yttrium", "Zinc", "Alchemy",
        "Botany", "Chemistry", "Dynamics", "Ecology", "Forestry", "Geology", "Hydrology", "Ichthyology", "Jurisprudence", "Kinetics",
        "Linguistics", "Meteorology", "Nephrology", "Oceanography", "Paleontology", "Quantum", "Radiology", "Seismology", "Toxicology", "Urology",
        "Virology", "Welfare", "Xenology", "Yoga", "Zoology", "Adventure", "Beauty", "Courage", "Dream", "Energy",
        "Freedom", "Growth", "Hope", "Inspiration", "Joy", "Knowledge", "Love", "Magic", "Nature", "Opportunity",
        "Peace", "Quest", "Romance", "Success", "Truth", "Unity", "Victory", "Wisdom", "Youth", "Achievement",
        "Belief", "Challenge", "Discovery", "Experience", "Future", "Goal", "Happiness", "Innovation", "Journey", "Kindness",
        "Leadership", "Motivation", "Nurture", "Optimism", "Passion", "Quality", "Resilience", "Strength", "Talent", "Understanding",
        "Value", "Wonder", "Excellence", "Faith", "Grace", "Harmony", "Integrity", "Justice", "Kindness", "Liberty",
        "Mercy", "Nobility", "Openness", "Patience", "Respect", "Serenity", "Trust", "Unity", "Virtue", "Warmth",
        "Xeniality", "Yearning", "Zeal", "Ambition", "Bravery", "Creativity", "Determination", "Empathy", "Fortitude", "Generosity",
        "Honesty", "Innovation", "Justice", "Kindness", "Loyalty", "Modesty", "Nobility", "Optimism", "Patience", "Quality",
        "Respect", "Sincerity", "Tolerance", "Understanding", "Virtue", "Wisdom", "Xeniality", "Youth", "Zeal", "Art",
        "Book", "City", "Dance", "Earth", "Fire", "Garden", "House", "Island", "Journey", "Kingdom",
        "Lake", "Mountain", "Ocean", "Palace", "River", "Sky", "Tree", "Valley", "Water", "Xenon",
        "Yacht", "Zoo", "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape", "Honeydew",
        "Kiwi", "Lemon", "Mango", "Orange", "Papaya", "Quince", "Raspberry", "Strawberry", "Tangerine", "Ugli",
        "Vanilla", "Watermelon", "Xigua", "Yam", "Zucchini", "Airplane", "Boat", "Car", "Dragon", "Elephant",
        "Falcon", "Giraffe", "Horse", "Iguana", "Jaguar", "Kangaroo", "Lion", "Monkey", "Nightingale", "Owl",
        "Penguin", "Quail", "Rabbit", "Snake", "Tiger", "Unicorn", "Vulture", "Whale", "Xenops", "Yak",
        "Zebra", "Actor", "Baker", "Chef", "Doctor", "Engineer", "Farmer", "Guard", "Hunter", "Inventor",
        "Judge", "Knight", "Lawyer", "Musician", "Nurse", "Officer", "Pilot", "Queen", "Ranger", "Scientist",
        "Teacher", "Undertaker", "Veterinarian", "Writer", "Xenologist", "Yoga", "Zoologist", "Angel", "Bishop", "Cardinal",
        "Deacon", "Elder", "Father", "Guardian", "Healer", "Innkeeper", "Judge", "Keeper", "Leader", "Master",
        "Noble", "Oracle", "Priest", "Queen", "Ruler", "Saint", "Teacher", "Unicorn", "Virtue", "Wizard",
        "Xenial", "Youth", "Zealot", "Academy", "Bank", "Castle", "Dungeon", "Empire", "Fortress", "Guild",
        "Hospital", "Institute", "Jail", "Kingdom", "Library", "Monastery", "Nation", "Office", "Palace", "Quarry",
        "Republic", "School", "Temple", "University", "Village", "Workshop", "Xenodochium", "Yard", "Zoo", "Amulet",
        "Book", "Crown", "Diamond", "Emerald", "Feather", "Gem", "Helmet", "Icon", "Jewel", "Key",
        "Locket", "Medal", "Necklace", "Orb", "Pearl", "Quill", "Ring", "Staff", "Tome", "Urn",
        "Vial", "Wand", "Xylophone", "Yarn", "Zither", "Arrow", "Bow", "Crossbow", "Dagger", "Epee",
        "Falchion", "Glaive", "Halberd", "Iron", "Javelin", "Knife", "Lance", "Mace", "Nunchaku", "Oar",
        "Pike", "Quarterstaff", "Rapier", "Sword", "Trident", "Unicorn", "Voulge", "Whip", "Xiphos", "Yari",
        "Zweihander", "Alchemy", "Brewing", "Cooking", "Divination", "Enchanting", "Farming", "Gardening", "Healing", "Inscription",
        "Jewelry", "Knitting", "Leatherworking", "Mining", "Navigation", "Oratory", "Pottery", "Quilting", "Riding", "Sailing",
        "Tailoring", "Underwater", "Ventriloquism", "Weaving", "Xenology", "Yoga", "Zoology", "Arena", "Battlefield", "Colosseum",
        "Dueling", "Expedition", "Fight", "Gymnasium", "Hunt", "Invasion", "Journey", "Knight", "League", "Match",
        "Naval", "Olympics", "Pilgrimage", "Quest", "Race", "Siege", "Tournament", "Underwater", "Voyage", "War",
        "Xenial", "Yacht", "Zoo", "Alchemy", "Biology", "Chemistry", "Dynamics", "Economics", "Finance", "Geography",
        "History", "Informatics", "Journalism", "Kinetics", "Linguistics", "Mathematics", "Neuroscience", "Oceanography", "Physics", "Quantum",
        "Robotics", "Sociology", "Technology", "Urban", "Virology", "Welfare", "Xenology", "Yoga", "Zoology", "Apple",
        "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape", "Honeydew", "Kiwi", "Lemon", "Mango",
        "Orange", "Papaya", "Quince", "Raspberry", "Strawberry", "Tangerine", "Ugli", "Vanilla", "Watermelon", "Xigua",
        "Yam", "Zucchini", "Acorn", "Bark", "Cedar", "Dogwood", "Elm", "Fir", "Ginkgo", "Hickory",
        "Ivy", "Juniper", "Koa", "Larch", "Maple", "Nectarine", "Oak", "Pine", "Quince", "Redwood",
        "Spruce", "Tulip", "Umbrella", "Vine", "Willow", "Xerophyte", "Yew", "Zebrawood", "Amber", "Bronze",
        "Copper", "Diamond", "Emerald", "Feldspar", "Gold", "Hematite", "Iron", "Jade", "Kyanite", "Lapis",
        "Malachite", "Nickel", "Opal", "Pearl", "Quartz", "Ruby", "Silver", "Topaz", "Uranium", "Vanadium",
        "Wulfenite", "Xenotime", "Yttrium", "Zinc", "Alchemy", "Botany", "Chemistry", "Dynamics", "Ecology", "Forestry",
        "Geology", "Hydrology", "Ichthyology", "Jurisprudence", "Kinetics", "Linguistics", "Meteorology", "Nephrology", "Oceanography", "Paleontology",
        "Quantum", "Radiology", "Seismology", "Toxicology", "Urology", "Virology", "Welfare", "Xenology", "Yoga", "Zoology"
    ];

    private string[] Types =
    [
        "Jottings", "Discussions", "Reflections", "Observations", "Insights",
        "Memories", "Thoughts", "Ideas", "Notes", "Journal",
        "Diary", "Log", "Record", "Account", "Chronicle",
        "Narrative", "Story", "Tale", "Report", "Summary"
    ];

    private string[] ColorCodes =
    [
        "#FF5733", "#33FF57", "#3357FF", "#FF33F5", "#F5FF33", "#33F5FF", "#FF3366", "#66FF33", "#3366FF", "#FF6633",
        "#6633FF", "#33FF66", "#FF3366", "#66FF33", "#3366FF", "#FF6633", "#6633FF", "#33FF66", "#FF3366", "#66FF33",
        "#E74C3C", "#2ECC71", "#3498DB", "#9B59B6", "#F39C12", "#1ABC9C", "#E67E22", "#34495E", "#95A5A6", "#F1C40F",
        "#E91E63", "#9C27B0", "#673AB7", "#3F51B5", "#2196F3", "#03A9F4", "#00BCD4", "#009688", "#4CAF50", "#8BC34A",
        "#CDDC39", "#FFEB3B", "#FFC107", "#FF9800", "#FF5722", "#795548", "#607D8B", "#FF1744", "#E040FB", "#3D5AFE",
        "#2979FF", "#00B0FF", "#00E5FF", "#1DE9B6", "#00E676", "#76FF03", "#C6FF00", "#FFEA00", "#FFC400", "#FF9100",
        "#FF3D00", "#8D6E63", "#546E7A", "#F50057", "#D500F9", "#651FFF", "#3D5AFE", "#2979FF", "#00B0FF", "#00E5FF",
        "#1DE9B6", "#00E676", "#76FF03", "#C6FF00", "#FFEA00", "#FFC400", "#FF9100", "#FF3D00", "#8D6E63", "#546E7A",
        "#FF1744", "#E040FB", "#3D5AFE", "#2979FF", "#00B0FF", "#00E5FF", "#1DE9B6", "#00E676", "#76FF03", "#C6FF00",
        "#FFEA00", "#FFC400", "#FF9100", "#FF3D00", "#8D6E63", "#546E7A", "#F50057", "#D500F9", "#651FFF", "#3D5AFE",
        "#2979FF", "#00B0FF", "#00E5FF", "#1DE9B6", "#00E676", "#76FF03", "#C6FF00", "#FFEA00", "#FFC400", "#FF9100"
    ];

    private Random random;

    public NoteTypeGenerator(int seed = 4308)
    {
        random = new(seed);
    }

    private NoteType CreateNext()
    {
        var adjective = Adjectives[random.Next(Adjectives.Length)];
        var noun = Nouns[random.Next(Nouns.Length)];
        var types = Types[random.Next(Types.Length)];
        var colorCodes = ColorCodes[random.Next(ColorCodes.Length)];

        return NoteType.Create(new NoteTypeCreationDto($"{types}", $"{adjective} {noun}", $"{colorCodes}"));
    }

    public IEnumerable<NoteType> GetNoteTypes()
    {
        HashSet<NoteType> types = new();
        var adjectivesCount = Adjectives.Distinct().Count();
        var nounCount = Nouns.Distinct().Count();
        var typesCount = Types.Distinct().Count();
        var colorCodesCount = ColorCodes.Distinct().Count();
        var maxCount = (int)(adjectivesCount * nounCount * typesCount * colorCodesCount * .6);

        while(types.Count < maxCount)
        {
            var noteType = CreateNext();
            if (types.Add(noteType)) yield return noteType;
        }
    }
}
