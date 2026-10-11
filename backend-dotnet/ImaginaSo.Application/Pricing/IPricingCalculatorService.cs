namespace ImaginaSo.Application.Pricing
{
    internal interface IPricingCalculatorService
    {
        PricingCalculationResult Calculate(PricingCalculationInput input);
    }
}
