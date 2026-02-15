using System;

public abstract class GoodsTransport
{
    protected string transportId;
    protected string transportDate;
    protected int transportRating;

    public GoodsTransport(string transportId, string transportDate, int transportRating)
    {
        this.transportId = transportId;
        this.transportDate = transportDate;
        this.transportRating = transportRating;
    }

    public string TransportId
    {
        get { return transportId; }
        set { transportId = value; }
    }

    public string TransportDate
    {
        get { return transportDate; }
        set { transportDate = value; }
    }

    public int TransportRating
    {
        get { return transportRating; }
        set { transportRating = value; }
    }

    public abstract string VehicleSelection();
    public abstract float CalculateTotalCharge();
}