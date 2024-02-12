namespace DebtTracker.API.Models.DTO
{
    public class CustomErrorDto
    {
        public static CustomErrorDto CreateCustomErrorDtoFromException(Exception ex)
        {
            var message =  ex.Message;
            return new CustomErrorDto
            {
                Message = "Something bad happened",
                Description = message
            };
        }

        public string Message { get; set; }
        public string Description { get; set; }
    }
}
