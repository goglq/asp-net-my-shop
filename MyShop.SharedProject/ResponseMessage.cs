namespace MyShop.SharedProject;

public class ResponseMessage<TResponseModel>
{
    public string? Message { get; set; }
    
    public bool IsSuccess { get; set; }
    
    public TResponseModel? Model { get; set; }

    public ResponseMessage(string? message, bool isSuccess, TResponseModel? model = default)
    {
        Message = message;
        IsSuccess = isSuccess;
        Model = model;
    }
}