public static class FloatExtension
{
	public static float Remap(float v, float from1, float to1, float from2, float to2)
	{
		return (v - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}