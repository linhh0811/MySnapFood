using Azure;
using Newtonsoft.Json;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Api.AppStart
{
    /// <summary>
    /// Hàm ghi lỗi toàn cầu
    /// </summary>
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="next"></param>
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException sqlEx)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                var errorResponse = new SiResponse<DefaultClass>
                {
                    Success = false,
                    Message = "",
                    BuildQuery = null,
                    Data = null,
                    Errors = new Error
                    {
                        Id = new[]
                        {
                            "Có lỗi trong quá trình cập nhật dữ liệu vào phần mềm."
                        }
                    },
                    StatusCode = StatusCode.Forbidden
                };
                List<string> error = HandleSQLException(sqlEx);

                if (error != null && error.Count > 0)
                {
                    errorResponse.Errors.Id = errorResponse.Errors.Id.Concat(error).ToArray();
                }

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;

                var errorResponse = new SiResponse<DefaultClass>
                {
                    Success = false,
                    Message = "",
                    BuildQuery = null,
                    Data = null,
                    Errors = new Error
                    {
                        Id = new[]
                        {
                            "",
                            ex.Message
                        }
                    },
                    StatusCode = StatusCode.Forbidden
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }

        private List<string> HandleSQLException(Microsoft.EntityFrameworkCore.DbUpdateException sqlEx)
        {
            var error = new List<string>();
            Exception? currentEx = sqlEx.InnerException;

            while (currentEx != null)
            {
                var errorMessage = currentEx.Message.ToLower();

                // Lỗi liên quan đến độ dài dữ liệu
                if (errorMessage.Contains("string or binary data would be truncated") ||
                    errorMessage.Contains("data would be truncated"))
                {
                    error.Add("Có một trường có độ dài vượt quá kích thước quy định.");
                }

                // Lỗi liên quan đến khóa chính
                else if (errorMessage.Contains("violation of primary key constraint") ||
                        errorMessage.Contains("duplicate key") ||
                        errorMessage.Contains("cannot insert duplicate key"))
                {
                    error.Add("Lỗi vi phạm khóa chính. Dữ liệu đã tồn tại.");
                }

                // Lỗi liên quan đến khóa ngoại
                else if (errorMessage.Contains("foreign key constraint") ||
                        errorMessage.Contains("reference constraint"))
                {
                    if (errorMessage.Contains("delete") || errorMessage.Contains("update"))
                    {
                        error.Add("Không thể xóa hoặc cập nhật vì dữ liệu đang được tham chiếu ở nơi khác.");
                    }
                    else
                    {
                        error.Add("Lỗi vi phạm ràng buộc khóa ngoại. Dữ liệu tham chiếu không tồn tại.");
                    }
                }

                // Lỗi NULL
                else if (errorMessage.Contains("cannot insert the value null") ||
                        errorMessage.Contains("cannot be null") ||
                        errorMessage.Contains("null value"))
                {
                    error.Add("Không thể để trống trường bắt buộc này.");
                }

                // Lỗi chuyển đổi kiểu dữ liệu
                else if (errorMessage.Contains("conversion failed") ||
                        errorMessage.Contains("invalid cast") ||
                        errorMessage.Contains("data type mismatch"))
                {
                    error.Add("Dữ liệu không đúng định dạng yêu cầu.");
                }

                // Lỗi timeout
                else if (errorMessage.Contains("timeout") ||
                        errorMessage.Contains("deadlock") ||
                        errorMessage.Contains("lock request"))
                {
                    error.Add("Thao tác bị hủy do xung đột hoặc quá thời gian thực thi.");
                }

                // Lỗi ràng buộc CHECK
                else if (errorMessage.Contains("check constraint") ||
                        errorMessage.Contains("violated check"))
                {
                    error.Add("Dữ liệu không thỏa mãn điều kiện ràng buộc kiểm tra.");
                }

                // Lỗi ràng buộc UNIQUE
                else if (errorMessage.Contains("unique constraint") ||
                        errorMessage.Contains("unique index") ||
                        errorMessage.Contains("duplicate entry"))
                {
                    error.Add("Dữ liệu này đã tồn tại trong hệ thống.");
                }

                // Lỗi kết nối
                else if (errorMessage.Contains("connection") ||
                        errorMessage.Contains("network") ||
                        errorMessage.Contains("server"))
                {
                    error.Add("Lỗi kết nối đến cơ sở dữ liệu. Vui lòng thử lại sau.");
                }

                // Lỗi quyền truy cập
                else if (errorMessage.Contains("permission") ||
                        errorMessage.Contains("access denied") ||
                        errorMessage.Contains("unauthorized"))
                {
                    error.Add("Không có quyền thực hiện thao tác này.");
                }

                // Lỗi giới hạn kích thước
                else if (errorMessage.Contains("exceeds maximum") ||
                        errorMessage.Contains("too long") ||
                        errorMessage.Contains("size limit"))
                {
                    error.Add("Dữ liệu vượt quá giới hạn cho phép.");
                }

                // Lỗi ràng buộc DEFAULT
                else if (errorMessage.Contains("default constraint") ||
                        errorMessage.Contains("default value"))
                {
                    error.Add("Lỗi vi phạm giá trị mặc định.");
                }

                // Lỗi không xác định
                else
                {
                    error.Add($"Lỗi không xác định: {currentEx.Message}");
                }

                currentEx = currentEx.InnerException;
            }

            // Xử lý trường hợp không có InnerException
            if (error.Count == 0)
            {
                if (sqlEx.Message.ToLower().Contains("optimistic concurrency"))
                {
                    error.Add("Dữ liệu đã bị thay đổi bởi người khác. Vui lòng làm mới và thử lại.");
                }
                else
                {
                    error.Add("Có lỗi xảy ra khi thực hiện thao tác với cơ sở dữ liệu.");
                }
            }

            return error;
        }
    }
}
