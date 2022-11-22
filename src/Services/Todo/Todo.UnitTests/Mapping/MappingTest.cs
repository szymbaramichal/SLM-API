using AutoMapper;

namespace Todo.UnitTests.Mapping;

public class MappingTest : IClassFixture<MappingTestFixture>
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IMapper _mapper;

    public MappingTest(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _configurationProvider = fixture.ConfigurationProvider;
    }

    [Fact]
    public void MappingProfile_Success()
    {
        _configurationProvider.AssertConfigurationIsValid();
    }
}
