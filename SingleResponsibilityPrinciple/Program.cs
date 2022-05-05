using static System.Console;

var j = new Journal();
j.AddEntry("I cried today");
j.AddEntry("I ate a bug");
WriteLine(j);

var p = new Persistence();
var filename = @".\..\..\..\journal.txt";
p.SaveToFile(j, filename, true);

public class Journal
{
    private readonly List<string> entires = new List<string>();
    private static int count = 0;

    public int AddEntry(string text)
    {
        entires.Add($"{++count}: {text}");
        return count;
    }

    public void RemoveEntry(int index)
    {
        entires.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entires);
    }

    #region Don't
    //Add to much responsibilities to the Journal class
    public void Save(string filename)
    {
        File.WriteAllText(filename, ToString());
    }

    public static Journal Load(string filename)
    {
        return null;
    }

    public void Load(Uri uri) { }
    #endregion
}

#region Do
public class Persistence
{
    public void SaveToFile(Journal j, string filename, bool overwrite = false)
    {
        if (overwrite || !File.Exists(filename))
            File.WriteAllText(filename, j.ToString());
    }
}
#endregion

