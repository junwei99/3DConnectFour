public class Move
{
    public int xCoordinate;
    public int yCoordinate;
    public int zCoordinate;

    public Move(int _x, int _y, int _z)
    {
        xCoordinate = _x;
        yCoordinate = _y;
        zCoordinate = _z;
    }

    public void setMove(int x, int y, int z)
    {
        xCoordinate = x;
        yCoordinate = y;
        zCoordinate = z;
    }

}