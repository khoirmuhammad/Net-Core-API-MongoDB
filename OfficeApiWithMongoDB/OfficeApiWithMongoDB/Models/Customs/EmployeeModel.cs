using MongoDB.Bson;

namespace OfficeApiWithMongoDB.Models.Customs
{
    public class EmployeeModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public decimal Salary { get; set; } = 0;
        public bool IsPermanent { get; set; }
        public IFormFile File { get; set; } = default!;
        public string FileString { get; set; } = string.Empty;
    }
}
