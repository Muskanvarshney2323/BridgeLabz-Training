using System;

public class Utility
{
    public GoodsTransport ParseDetails(string input)
    {
        string[] parts = input.Split(':');
        string transportId = parts[0];
        string transportDate = parts[1];
        int transportRating = int.Parse(parts[2]);
        string transportType = parts[3];

        if (!ValidateTransportId(transportId))
        {
            Console.WriteLine("Transport id " + transportId + " is invalid");
            Console.WriteLine("Please provide a valid record");
            return null;
        }

        if (transportType.ToLower() == "bricktransport")
        {
            float brickSize = float.Parse(parts[4]);
            int brickQuantity = int.Parse(parts[5]);
            float brickPrice = float.Parse(parts[6]);
            return new BrickTransport(transportId, transportDate, transportRating, brickSize, brickQuantity, brickPrice);
        }
        else if (transportType.ToLower() == "timbertransport")
        {
            float timberLength = float.Parse(parts[4]);
            float timberRadius = float.Parse(parts[5]);
            string timberType = parts[6];
            float timberPrice = float.Parse(parts[7]);
            return new TimberTransport(transportId, transportDate, transportRating, timberLength, timberRadius, timberType, timberPrice);
        }
        return null;
    }

    public bool ValidateTransportId(string transportId)
    {
        if (transportId.Length != 7) return false;
        if (!transportId.StartsWith("RTS")) return false;
        for (int i = 3; i < 6; i++)
        {
            if (!char.IsDigit(transportId[i])) return false;
        }
        if (!char.IsUpper(transportId[6]) || !char.IsLetter(transportId[6])) return false;
        return true;
    }

    public string FindObjectType(GoodsTransport goodsTransport)
    {
        if (goodsTransport is TimberTransport)
            return "TimberTransport";
        else if (goodsTransport is BrickTransport)
            return "BrickTransport";
        return "";
    }
}