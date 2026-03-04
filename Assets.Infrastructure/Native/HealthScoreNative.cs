using System.Runtime.InteropServices;

namespace Assets.Infrastructure.Native;

public static class HealthScoreNative
{
    // Linux: libhealthscore.so
    [DllImport("libhealthscore.so", EntryPoint = "calculate_health_score")]
    public static extern int CalculateHealthScore(int temperature, int vibration, int pressure);
}