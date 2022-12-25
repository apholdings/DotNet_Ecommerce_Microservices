namespace ProductAPI.Models.DTO.ImageDtos
{
	public class ImageUpdateDTO
	{
		public int ImageId { get; set; }
		public string OwnerId { get; set; }
		public int ProductId { get; set; }
		public string Url { get; set; }
	}
}
