namespace PricingTool.MVVM.Models;


public class ProjectData
{
    public object[,] dataLDC { get; set; }
    public object[,] dataLPA { get; set; }
    public List<List<object>> dataLPL { get; set; }
    public string regLPL { get; set; }
    public string plateLPL { get; set; }
    public string plateKidsLPL { get; set; }
    public List<List<object>> dataLAC { get; set; }
    public List<List<object>> dataTrave { get; set; }
    //public string dataLabel { get; set; }
    public string pospadValue { get; set; }
    public List<List<object>> dataLKK { get; set; }
}
