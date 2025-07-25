namespace Domain.Entities;

public class ProjectEntity
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<ChartEntity>? Charts { get; set; } = new();
}

public class ChartEntity
{
    public string Symbol { get; set; }
    public string Timeframe { get; set; }
    public List<IndicatorEntity>? Indicators { get; set; } = new();
}

public class IndicatorEntity
{
    public string Name { get; set; }
    public string Parameters { get; set; }
}