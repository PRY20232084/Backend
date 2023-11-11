namespace PRY20232084.DTOs
{
    public class RawMaterialResponseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string Color { get; set; }
        public decimal Stock { get; set; }
        public int MeasurementUnit_ID { get; set; }
        public string MeasurementUnitName { get; set; }
        public string CreatedBy { get; set; }
    }
}
