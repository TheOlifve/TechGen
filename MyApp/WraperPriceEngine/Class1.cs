using System.Reflection;
using ACA.PriceEngine;

namespace WraperPriceEngine;

public class WrapPriceEngine
{
    PriceEngine _engine = new PriceEngine();
    
    public decimal CalculatePayable(PriceInput input)
    {
        Type type =  typeof(PriceEngine);

        decimal amount = (decimal)type.GetMethod("ComputeSubtotal", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(_engine, new object[] { input.Lines});
        
        amount = (decimal)type.GetMethod("ApplyVolumeDiscount", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(_engine, new object[] { amount,
            type.GetMethod("CountUnits",  BindingFlags.NonPublic | BindingFlags.Instance).Invoke(_engine, new object[] { input.Lines }) });
        
        amount = (decimal)type.GetMethod("ApplyLoyaltyDiscount", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(_engine, new object[] { amount, input.LoyaltyTier });
        
        amount = (decimal)type.GetMethod("ApplyCoupon", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(_engine, new object[] { amount, input.CouponAmount });
        
        amount = (decimal)type.GetMethod("ApplyVat", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(_engine, new object[] { amount, input.VatRate });
        
        return (decimal)type.GetMethod("RoundMoney", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { amount });
    }
    
}