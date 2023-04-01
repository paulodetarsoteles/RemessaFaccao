
int f()
{
    int result = 0;
    for (int k = 0; k < 5; k++)
    {
        if ( k % 3 == 1)
            result = result + k; 
        else
            result = result + 1;
    }
    return result;
}

Console.WriteLine(f());