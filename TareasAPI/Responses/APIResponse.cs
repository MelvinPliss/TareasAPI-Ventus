namespace TareasAPI.Responses
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public T? Value { get; set; }
        public List<T>? Datos { get; set; }
        public string Msg { get; set; } = string.Empty;
        public int? TotalRecords { get; set; }
        public int? TotalPages { get; set; }
    }
}
