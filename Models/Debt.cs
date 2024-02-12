namespace DebtTracker.API.Models
{
    public class Debt
    {
        private string _id;

        public string Id
        {
            get { return _id ??= GenerateGUIDBasedID(); }
        }

        public decimal AmountOfDebt { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsOverdue => DateTime.UtcNow > DueDate;
        public string ClientName { get; set; }
        public string CreditorName { get; set; }

        private string GenerateGUIDBasedID()
        {
            var uniquePart = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            var id = $"{CreditorName}-{ClientName}-{uniquePart}";

            return id;
        }

      
        public bool Equals(Debt obj)
        {
            return obj != null && obj.AmountOfDebt == this.AmountOfDebt;
        }

    }
}
