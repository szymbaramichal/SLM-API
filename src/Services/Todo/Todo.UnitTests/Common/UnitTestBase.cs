using AutoMapper;
using MongoDB.Driver.Core.Misc;
using Moq;
using Todo.API.Data;
using Todo.API.Repositories.Interfaces;
using Todo.UnitTests.Mapping;

namespace Todo.UnitTests.Common;

public class UnitTestBase : IClassFixture<MappingTestFixture>
{
    protected readonly Mock<ITodoRepository> _repoMock;
    protected readonly Mock<IMapper> _mapperMock;
    protected readonly IMapper _mapper;

    public UnitTestBase(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _repoMock = new Mock<ITodoRepository>();
        _mapperMock = new Mock<IMapper>();
    }
}
