using PRY20232084.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PRY20232084.DTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size_ID { get; set; }
        public int Style_ID { get; set; }
        public int Stock { get; set; }
        public string CreatedBy { get; set; }
        public List<CreateProductDetailDTO> productDetailDTOs { get; set; }
	}

	public class ProductCreateDTO2
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Size_ID { get; set; }
		public int Style_ID { get; set; }
		public int Stock { get; set; }
		public string CreatedBy { get; set; }
	}
}
