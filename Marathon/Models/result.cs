namespace Marathon.Models;

public class ResultCollection
{
    public result[] results { get; set; }
}
    
public class result
{
    public string name     { get; set; }
    public string placement { get; set; }
    public string time     { get; set; }

    public string detail
    { get { return placement + " Place         Time: " + time; } }
}