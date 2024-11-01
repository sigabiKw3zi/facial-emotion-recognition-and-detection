namespace FacialEmotionRecognition.APIs.Dtos;

public class DatasetWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public List<string>? Images { get; set; }

    public string? Name { get; set; }

    public string? Source { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
