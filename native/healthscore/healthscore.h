#ifndef HEALTHSCORE_H
#define HEALTHSCORE_H

#ifdef __cplusplus
extern "C" {
#endif

// ANSI C function exported from shared library.
// Returns an integer health score in range 0..100.
int calculate_health_score(int temperature, int vibration, int pressure);

#ifdef __cplusplus
}
#endif

#endif
