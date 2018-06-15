

public class ShopItem {

    private string id;
	private string speed;
    private string rotate;
    private string model;
    private string price;

    /// <summary>
    /// 构造函数
    /// </summary>  
    public ShopItem(string id, string speed, string rotate, string model, string price)
    {
        this.id = id;
        this.speed = speed;
        this.rotate = rotate;
        this.model = model;
        this.price = price;
    }

    public string ID
    {
        get { return id; }
        set { id = value; }
    }
    public string Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public string Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }

    public string Model
    {
        get { return model; }
        set { model = value; }
    }


    public string Price
    {
        get { return price; }
        set { price = value; }
    }

    public override string ToString()
    {
        return string.Format("Speed:{0},Rotate:{1},Model:{2},Price:{3}", speed, rotate, model, price);
    }
}
