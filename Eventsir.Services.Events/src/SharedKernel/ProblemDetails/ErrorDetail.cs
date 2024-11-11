namespace SharedKernel
{
    public class ErrorDetail
    {
        public ErrorDetail(string detail, string pointer)
        {
            Detail = detail;
            Pointer = pointer;
        }
        public string Detail { get; set; }
        public string Pointer { get; set; }
    }
}
