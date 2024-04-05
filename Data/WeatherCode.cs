namespace WeatherChecker.Data;

public enum WeatherCode
{
	UNKNOWN = -1,

	ClearSky = 0,
	Mainly_Clear = 1,
	Partly_Cloud = 2,
	Overcast = 3,

	Fog = 45,
	RimeFog = 48,

	LightDrizzle = 51,
	ModerateDrizzle = 53,
	DenseDrizzle = 55,

	LightFreezingDrizzle = 56,
	DenseFreezingDrizzle = 57,

	LightRain = 61,
	ModerateRain = 63,
	HeavyRain = 65,

	LightFreezingRain = 66,
	HeavyFreezingRain = 67,

	LightSnow = 71,
	ModerateSnow = 73,
	HeavySnow = 75,

	SnowGrains = 77,

	LightRainShower = 80,
	ModerateRainShower = 81,
	ViolentRainShower = 82,

	LightSnowShower = 85,
	HeavySnowShower = 86,

	Thunderstorm = 95,
	ThunderstormWithLightHail = 96,
	ThunderstormWithHeavyHail = 99
}
