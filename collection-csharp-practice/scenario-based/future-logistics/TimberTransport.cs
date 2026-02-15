using System;

public class TimberTransport : GoodsTransport
{
    private float timberLength;
    private float timberRadius;
    private string timberType;
    private float timberPrice;

    public TimberTransport(string transportId, string transportDate, int transportRating, float timberLength, float timberRadius, string timberType, float timberPrice)
        : base(transportId, transportDate, transportRating)
    {
        this.timberLength = timberLength;
        this.timberRadius = timberRadius;
        this.timberType = timberType;
        this.timberPrice = timberPrice;
    }

    public float TimberLength
    {
        get { return timberLength; }
        set { timberLength = value; }
    }

    public float TimberRadius
    {
        get { return timberRadius; }
        set { timberRadius = value; }
    }

    public string TimberType
    {
        get { return timberType; }
        set { timberType = value; }
    }

    public float TimberPrice
    {
        get { return timberPrice; }
        set { timberPrice = value; }
    }

    public override string VehicleSelection()
    {
        float area = 2 * 3.147f * timberRadius * timberLength;
        if (area < 250)
            return "Truck";
        else if (area >= 250 && area <= 400)
            return "Lorry";
        else
            return "MonsterLorry";
    }

    public override float CalculateTotalCharge()
    {
        float volume = 3.147f * timberRadius * timberRadius * timberLength;
        float typeMultiplier = timberType.ToLower() == "premium" ? 0.25f : 0.15f;
        float price = volume * timberPrice * typeMultiplier;
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