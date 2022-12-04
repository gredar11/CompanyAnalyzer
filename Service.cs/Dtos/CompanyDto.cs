namespace Service.Dtos
{
    public class CompanyDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateOnly EstablishmentDate { get; set; }
        public string Adress { get; set; }
    }
}
