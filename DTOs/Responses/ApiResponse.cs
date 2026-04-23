namespace LibraryManagementSystem.DTOs.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public int? TotalCount { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Success", int? totalCount = null)
            => new() { Success = true, Message = message, Data = data, TotalCount = totalCount };

        public static ApiResponse<T> Fail(string message)
            => new() { Success = false, Message = message };

        public static ApiResponse<T> Created(T data, string message = "Record created successfully.")
            => new() { Success = true, Message = message, Data = data };

        public static ApiResponse<T> Updated(T data, string message = "Record updated successfully.")
            => new() { Success = true, Message = message, Data = data };

        public static ApiResponse<T> Deleted(T data, string message = "Record deleted successfully.")
            => new() { Success = true, Message = message, Data = data };
    }
}
