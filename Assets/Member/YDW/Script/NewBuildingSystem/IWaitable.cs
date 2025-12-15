namespace Member.YDW.Script.NewBuildingSystem
{
    public interface IWaitable
    {
        public bool IsWaiting { get;}

        public void SetWaiting(bool waiting);

    }
}