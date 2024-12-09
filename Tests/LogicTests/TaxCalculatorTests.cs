using Xunit;
using FluentAssertions;
using testing_final.Logic.Utilities;

namespace testing_final.Tests.LogicTests;

public class TaxCalculatorTests
{
    [Fact]
    public void CalculateTax_ShouldReturnCorrectTaxAmount()
    {
        // Arrange
        decimal subtotal = 100.00m;
        decimal taxRate = 0.07m;

        // Act
        var tax = TaxCalculator.CalculateTax(subtotal, taxRate);

        // Assert
        tax.Should().Be(7.00m);
    }

    [Fact]
    public void CalculateTax_ShouldReturnZeroForZeroSubtotal()
    {
        // Arrange
        decimal subtotal = 0.00m;
        decimal taxRate = 0.07m;

        // Act
        var tax = TaxCalculator.CalculateTax(subtotal, taxRate);

        // Assert
        tax.Should().Be(0.00m);
    }
}
