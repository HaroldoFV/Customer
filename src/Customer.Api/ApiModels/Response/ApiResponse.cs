namespace Customer.Api.ApiModels.Response;

public class ApiResponse<TData>(TData data)
{
    public TData Data { get; private set; } = data;
}