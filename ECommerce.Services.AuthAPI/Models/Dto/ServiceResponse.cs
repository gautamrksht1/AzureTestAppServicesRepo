namespace ECommerce.Services.AuthAPI.Models.Dto
{
    public class ServiceResponse<T>
    {
        public T Result { get; set; }
        public bool HasErrors { get; set; } 
        public List<string> Errors { get; set; } = new List<string>();
    }
}
