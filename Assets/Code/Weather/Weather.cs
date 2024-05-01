public abstract class Weather
{
    protected SkyboxManager skyboxManager;

    public Weather(SkyboxManager skyboxManager)
    {
        this.skyboxManager = skyboxManager;
    }

    public abstract void Start();
    public abstract void Stop();
}

public class Rainy : Weather
{
    public Rainy(SkyboxManager skyboxManager) : base(skyboxManager) { }

    public override void Start()
    {
        skyboxManager.ChangeSkybox("rain");
        // Start raining effects, instantiate rain particles, etc.
    }

    public override void Stop()
    {
        // Stop raining effects
    }
}

public class Sunny : Weather
{
    public Sunny(SkyboxManager skyboxManager) : base(skyboxManager) { }

    public override void Start()
    {
        skyboxManager.ChangeSkybox("sun");
        // Additional sunny weather effects
    }

    public override void Stop()
    {
        // Clean up sunny weather effects
    }
}

public class Windy : Weather
{
    public Windy(SkyboxManager skyboxManager) : base(skyboxManager) { }

    public override void Start()
    {
        skyboxManager.ChangeSkybox("wind");
        // Additional windy weather effects
    }

    public override void Stop()
    {
        // Clean up windy weather effects
    }
}