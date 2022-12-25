namespace ProductAPI.Models.DTO.VideoDtos
{
	public class VideoCreateDTO
	{
		public string OwnerId { get; set; }
		public int ProductId { get; set; }
		public string Url { get; set; }
		public string Title { get; set; }
	}
}
