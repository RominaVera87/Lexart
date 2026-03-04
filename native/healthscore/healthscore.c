#include "healthscore.h"

static int clamp_int(int value, int min, int max)
{
    if (value < min) return min;
    if (value > max) return max;
    return value;
}

int calculate_health_score(int temperature, int vibration, int pressure)
{
    // Simple scoring:
    // - Start from 100
    // - Penalize based on absolute deviation from nominal ranges.
    // This is intentionally simple for the technical test.
    int score = 100;

    // Temperature nominal ~ 70 (arbitrary)
    int temp_penalty = (temperature > 70) ? (temperature - 70) : (70 - temperature);
    // Vibration nominal ~ 10
    int vib_penalty = (vibration > 10) ? (vibration - 10) * 2 : (10 - vibration) * 2;
    // Pressure nominal ~ 30
    int press_penalty = (pressure > 30) ? (pressure - 30) : (30 - pressure);

    score -= (temp_penalty + vib_penalty + press_penalty);

    return clamp_int(score, 0, 100);
}
