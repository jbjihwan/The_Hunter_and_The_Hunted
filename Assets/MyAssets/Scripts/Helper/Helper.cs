using UnityEngine;

public static class Helper
{
    public static void Swap<T>(ref T v1, ref T v2)
    {
        T temp = v1;
        v1 = v2;
        v2 = temp;
    }

    public static void Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Swap(ref array[i], ref array[randomIndex]);
        }
    }
}
