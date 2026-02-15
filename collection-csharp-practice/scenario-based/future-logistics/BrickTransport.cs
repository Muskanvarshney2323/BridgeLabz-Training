using System;

public class BrickTransport : GoodsTransport
{
    private float brickSize;
    private int brickQuantity;
    private float brickPrice;

    public BrickTransport(string transportId, string transportDate, int transportRating, float brickSize, int brickQuantity, float brickPrice)
        : base(transportId, transportDate, transportRating)
    {
        this.brickSize = brickSize;
        this.brickQuantity = brickQuantity;
        this.brickPrice = brickPrice;
    }

    public float BrickSize
    {
        get { return brickSize; }
        set { brickSize = value; }
    }

    public int BrickQuantity
    {
        get { return brickQuantity; }
        set { brickQuantity = value; }
    }

    public float BrickPrice
    {
        get { return brickPrice; }
        set { brickPrice = value; }
    }

    public override string VehicleSelection()
    {
        if (brickQuantity < 300)
            return "Truck";
        else if (brickQuantity >= 300 && brickQuantity <= 500)
            return "Lorry";
        else
            return "MonsterLorry";
    }

    public override float CalculateTotalCharge()
    {
        float price = brickPrice * brickQuantity;
        float tax = price * 0.3f;
        float discountPercentage = 0;
        if (transportRating == 5)
            discountPercentage = 0.20f;
        else if (transportRating == 3 || transportRating == 4)
            discountPercentage = 0.10f;
        float discount = price * discountPercentage;
        float vehiclePrice = 0;
        string vehicle = VehicleSelection().ToLower();
        if (vehicle == "truck")
            vehiclePrice = 1000;
        else if (vehicle == "lorry")
            vehiclePrice = 1700;
        else if (vehicle == "monsterlorry")
            vehiclePrice = 3000;
        return price + vehiclePrice + tax - discount;
    }
}