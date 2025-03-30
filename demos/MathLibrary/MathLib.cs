namespace MathLibrary;

public class MathLib
{
    public int Addition(int x, int y)
    {
        if (x < 0) throw new ArgumentOutOfRangeException("x");
        if (y < 0) throw new ArgumentOutOfRangeException("y");

        return x + y;
    }
}
