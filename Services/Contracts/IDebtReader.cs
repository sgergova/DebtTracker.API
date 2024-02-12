using DebtTracker.API.Models.DTO;

namespace DebtTracker.API.Services.Contracts
{
    public interface IDebtReader
    {
        Task<Dictionary<string, List<string>>> ProcessDebts(MemoryStream debts);
    }
}
