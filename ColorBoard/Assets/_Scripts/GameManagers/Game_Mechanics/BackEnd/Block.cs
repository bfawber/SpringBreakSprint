
public class Block {

	private int[] _position = new int[2];
    private Board.COLOR _color;

    public Block(int x, int y, Board.COLOR color)
    {
        this._position[0] = x;
        this._position[1] = y;
        this._color = color;
    }

    public void setColor(Board.COLOR color)
    {
        this._color = color;
    }

    public Board.COLOR getColor()
    {
        return this._color;
    }

    public void setPosition(int x, int y)
    {
        this._position[0] = x;
        this._position[1] = y;
    }

}
