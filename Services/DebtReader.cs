using DebtTracker.API.Services.Contracts;

namespace DebtTracker.API.Services
{
    public class DebtReader : IDebtReader
    {
        public async Task<Dictionary<string, List<string>>> ProcessDebts(MemoryStream debts)
        {

            var parsedDebts = new Dictionary<string, List<string>>();

            var isFileValid = await ValidateInput(debts);

            if (!isFileValid)
            {
                throw new ArgumentException(Common.Constants.NumberOfColumn);
            }

            debts.Position = 0;

            using (var reader = new StreamReader(debts))
            {
                var keys = reader.ReadLine()?.Split('|');


                if (keys == null)
                    return [];

                foreach (var key in keys)
                {
                    parsedDebts[key] = [];
                }

                if (!ValidateHeader(keys))
                {
                    throw new ArgumentException(Common.Constants.ColumnNames);
                }

                string line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var values = line.Split('|');

                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (string.IsNullOrEmpty(values[i]))
                        {
                            parsedDebts[keys[i]].Add(string.Empty);
                        }
                        else
                        {
                            parsedDebts[keys[i]].Add(values[i]);
                        }
                    }
                }
            }

            return parsedDebts;
        }

        #region Validation helpers

        private async Task<bool> ValidateInput(MemoryStream debts)
        {
            debts.Position = 0;

            var file = await new StreamReader(debts).ReadToEndAsync();

            string[] lines = file.Split('\n');

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] columns = line.Split('|');
                if (columns.Length != 4)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateHeader(string[] keys)
        {

            var requiredKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                 {
                     "Amount of debt",
                     "Due date",
                     "Client name",
                     "Creditor name"
                 };

            var inputKeys = new HashSet<string>(keys.Select(key => key.Trim()), StringComparer.OrdinalIgnoreCase);

            bool isValid = requiredKeys.SetEquals(inputKeys);

            return isValid;

        }

        #endregion
    }
}
