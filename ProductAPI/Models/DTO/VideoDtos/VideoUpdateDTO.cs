namespace ProductAPI.Models.DTO.VideoDtos
{
	public class VideoUpdateDTO
	{
		public int VideoId { get; set; }
		public string OwnerId { get; set; }
		public int ProductId { get; set; }
		public string Url { get; set; }
	}
}
