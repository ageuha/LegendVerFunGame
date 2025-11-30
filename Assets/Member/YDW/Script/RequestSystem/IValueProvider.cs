namespace Member.YDW.Script.RequestSystem
{
    public interface IValueProvider<in TRequestValue, out TResultValue>
    {
        TResultValue GetValue(TRequestValue requestValue);
    }
}