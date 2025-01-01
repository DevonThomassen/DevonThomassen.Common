using DevonThomassen.Common.Extensions;

namespace DevonThomassen.Common.Tests.Extensions;

public sealed class EnumerableExtensionTests
{
    [Fact]
    public void IsNullOrEmpty_ListIsNull_ReturnsTrue()
    {
        // Arrange
        List<int>? list = null;
        
        // Act
        var result = list.IsNullOrEmpty();
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void IsNullOrEmpty_ListIsEmpty_ReturnsTrue()
    {
        // Arrange
        List<int> list = [];
        
        // Act
        var result = list.IsNullOrEmpty();
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void IsNullOrEmpty_ListIsNotEmpty_ReturnsFalse()
    {
        // Arrange
        List<int> list = [ 1, 2, 3 ];
        
        // Act
        var result = list.IsNullOrEmpty();
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void IsNullOrEmpty_IEnumerableIsNull_ReturnsTrue()
    {
        // Arrange
        IEnumerable<string>? list = null;
        
        // Act
        var result = list.IsNullOrEmpty();
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void IsNullOrEmpty_IEnumerableIsEmpty_ReturnsTrue()
    {
        // Arrange
        IEnumerable<string> list = [];
        
        // Act
        var result = list.IsNullOrEmpty();
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void IsNullOrEmpty_IEnumerableIsNotEmpty_ReturnsFalse()
    {
        // Arrange
        IEnumerable<string> list = [ "1", "2", "3" ];
        
        // Act
        var result = list.IsNullOrEmpty();
        
        // Assert
        Assert.False(result);
    }
}