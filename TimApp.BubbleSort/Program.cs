int[] arr = { 11, 93, 45, 98, 13, 55 };

int dummy = 0;

for (int i = 0; i < arr.Length; i++)
{
    bool endSort = true;
    for (int x = 0; x < arr.Length - 1; x++)
    {

        if (arr[x] > arr[x + 1])
        {
            dummy = arr[x + 1];
            arr[x + 1] = arr[x];
            arr[x] = dummy;

            Console.WriteLine(String.Join(" ", arr.Select(s => s).ToArray()));
            endSort = false;
        }
    }
    if (endSort) break;
}

Console.ReadKey();