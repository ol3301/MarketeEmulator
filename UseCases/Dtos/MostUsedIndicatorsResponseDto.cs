namespace UseCases.Dtos;

public class MostUsedIndicatorsResponseDto
{
    public IEnumerable<MostUsedIndicatorEntryResponseDto> Indicators { get; set; }
}

public class MostUsedIndicatorEntryResponseDto
{
    public string Name { get; set; }
    public int Used { get; set; }
}