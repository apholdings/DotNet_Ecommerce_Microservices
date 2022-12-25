﻿namespace ProductAPI.Models
{
	public class Pagination
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 20;
		public int TotalItems { get; set; }
		public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
	}
}
