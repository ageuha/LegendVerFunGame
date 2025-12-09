namespace Member.JJW.Code.Weather
{
    public class Sunny : Weather
    {
        public override void OnStart()
        {
            FadeToTargetColor(targetColor);
        }

        public override void OnStop()
        {
            
        }
    }
}