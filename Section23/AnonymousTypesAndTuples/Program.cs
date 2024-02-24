class Program
{
    static void Main()
    {
        var anonType = new { Prop1 = "testString", Prop2 = 2 };
        var anonTypeArray = new[] { new { Prop1 = "", Prop2 = 2 }, new { Prop1 = "", Prop2 = 2 } };
        var anonTypeNested = new { Prop1 = anonType, Prop2 = anonTypeArray };
        Tuple<int?, string> tuple = new Tuple<int?, string>(null, "word");
    }
}
