using AutoMapper;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Requests;
using MedMateAI.Application.DTOs.DiseasePriorProbabilities.Responses;
using MedMateAI.Application.Service;
using MedMateAI.Domain.Common;
using MedMateAI.Domain.Entities;
using MedMateAI.Domain.Persistence;
using MedMateAI.Domain.Repository;
using Moq;
using NUnit.Framework;

namespace MedMateAI.Tests.Services;

[TestFixture]
public class DiseasePriorProbabilityServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock = null!;
    private Mock<IDiseasePriorProbabilityRepository> _repoMock = null!;
    private Mock<IMapper> _mapperMock = null!;
    private DiseasePriorProbabilityService _service = null!;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repoMock = new Mock<IDiseasePriorProbabilityRepository>();
        _mapperMock = new Mock<IMapper>();

        _unitOfWorkMock.Setup(u => u.DiseasePriorProbabilities).Returns(_repoMock.Object);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _mapperMock.Setup(m => m.Map<DiseasePriorProbabilityResponse>(It.IsAny<DiseasePriorProbability>()))
            .Returns((DiseasePriorProbability src) => new DiseasePriorProbabilityResponse
            {
                Id = src.Id,
                Icd10Code = src.Icd10Code,
                DiseaseName = src.DiseaseName,
                PA = src.PA,
                IsActive = src.IsActive,
                CreatedAt = src.CreatedAt,
                UpdatedAt = src.UpdatedAt,
            });

        _service = new DiseasePriorProbabilityService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Test]
    [Category("N")]
    public async Task ListAsync_ValidRequest_ReturnsPagedResponse()
    {
        var entity = new DiseasePriorProbability
        {
            Id = Guid.NewGuid(),
            Icd10Code = "J06.9",
            DiseaseName = "URI",
            PA = 0.15,
            IsActive = true,
        };
        var pagedResult = new PagedResult<DiseasePriorProbability>
        {
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 1,
            TotalPages = 1,
            Items = new List<DiseasePriorProbability> { entity },
        };

        _repoMock.Setup(r => r.GetPagedAsync(
                1, 10,
                It.IsAny<System.Linq.Expressions.Expression<Func<DiseasePriorProbability, bool>>>(),
                It.IsAny<Func<IQueryable<DiseasePriorProbability>, IOrderedQueryable<DiseasePriorProbability>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagedResult);

        var result = await _service.ListAsync(1, 10);

        Assert.That(result.TotalCount, Is.EqualTo(1));
        Assert.That(result.Items, Has.Count.EqualTo(1));
        Assert.That(result.Items[0].Icd10Code, Is.EqualTo("J06.9"));
    }

    [Test]
    [Category("A")]
    public async Task GetByIdAsync_EmptyId_ReturnsNull()
    {
        var result = await _service.GetByIdAsync(Guid.Empty);

        Assert.That(result, Is.Null);
        _repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    [Category("A")]
    public async Task GetByIdAsync_Deleted_ReturnsNull()
    {
        var id = Guid.NewGuid();
        _repoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DiseasePriorProbability { Id = id, IsDeleted = true });

        var result = await _service.GetByIdAsync(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    [Category("N")]
    public async Task GetByIdAsync_Existing_ReturnsResponse()
    {
        var id = Guid.NewGuid();
        _repoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DiseasePriorProbability
            {
                Id = id,
                Icd10Code = "J06.9",
                PA = 0.2,
                IsActive = true,
            });

        var result = await _service.GetByIdAsync(id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Icd10Code, Is.EqualTo("J06.9"));
        Assert.That(result.PA, Is.EqualTo(0.2));
    }

    [Test]
    [Category("B")]
    public async Task BulkCreateAsync_EmptyItems_ReturnsError()
    {
        var request = new BulkCreateDiseasePriorProbabilitiesRequest
        {
            Items = new List<CreateDiseasePriorProbabilityRequest>(),
        };

        var (succeeded, errors, data) = await _service.BulkCreateAsync(request);

        Assert.That(succeeded, Is.False);
        Assert.That(errors, Contains.Item("Cần ít nhất một bản ghi P(A)."));
        Assert.That(data, Is.Null);
    }

    [Test]
    [Category("A")]
    public async Task BulkCreateAsync_NullItem_ReturnsIndexedError()
    {
        var request = new BulkCreateDiseasePriorProbabilitiesRequest
        {
            Items = new List<CreateDiseasePriorProbabilityRequest> { null! },
        };

        var (succeeded, errors, data) = await _service.BulkCreateAsync(request);

        Assert.That(succeeded, Is.False);
        Assert.That(errors, Contains.Item("Items[0]: Bản ghi là bắt buộc."));
    }

    [Test]
    [Category("A")]
    public async Task BulkCreateAsync_InvalidPa_ReturnsError()
    {
        var request = new BulkCreateDiseasePriorProbabilitiesRequest
        {
            Items = new List<CreateDiseasePriorProbabilityRequest>
            {
                new() { Icd10Code = "J06.9", PA = 1.5 },
            },
        };

        var (succeeded, errors, data) = await _service.BulkCreateAsync(request);

        Assert.That(succeeded, Is.False);
        Assert.That(errors.Any(e => e.Contains("PA phải nằm trong khoảng (0, 1].")), Is.True);
    }

    [Test]
    [Category("A")]
    public async Task BulkCreateAsync_DuplicateCodeWithinRequest_ReturnsError()
    {
        var request = new BulkCreateDiseasePriorProbabilitiesRequest
        {
            Items = new List<CreateDiseasePriorProbabilityRequest>
            {
                new() { Icd10Code = "J06.9", PA = 0.1 },
                new() { Icd10Code = "j06.9", PA = 0.2 },
            },
        };

        var (succeeded, errors, data) = await _service.BulkCreateAsync(request);

        Assert.That(succeeded, Is.False);
        Assert.That(errors.Any(e => e.Contains("bị trùng trong request")), Is.True);
    }

    [Test]
    [Category("A")]
    public async Task BulkCreateAsync_CodeExistsInDb_ReturnsError()
    {
        var request = new BulkCreateDiseasePriorProbabilitiesRequest
        {
            Items = new List<CreateDiseasePriorProbabilityRequest>
            {
                new() { Icd10Code = "J06.9", PA = 0.15 },
            },
        };

        _repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<DiseasePriorProbability>
            {
                new() { Icd10Code = "J06.9", IsDeleted = false },
            });

        var (succeeded, errors, data) = await _service.BulkCreateAsync(request);

        Assert.That(succeeded, Is.False);
        Assert.That(errors.Any(e => e.Contains("đã tồn tại")), Is.True);
    }

    [Test]
    [Category("N")]
    public async Task BulkCreateAsync_AllValid_PersistsAllAndReturnsSortedResponses()
    {
        var request = new BulkCreateDiseasePriorProbabilitiesRequest
        {
            Items = new List<CreateDiseasePriorProbabilityRequest>
            {
                new() { Icd10Code = "j18.9", DiseaseName = "Pneumonia", PA = 0.08, IsActive = true },
                new() { Icd10Code = "J06.9", DiseaseName = "URI", PA = 0.15 },
            },
        };

        _repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<DiseasePriorProbability>());

        var (succeeded, errors, data) = await _service.BulkCreateAsync(request);

        Assert.That(succeeded, Is.True);
        Assert.That(errors, Is.Empty);
        Assert.That(data, Has.Count.EqualTo(2));
        Assert.That(data![0].Icd10Code, Is.EqualTo("J06.9"));
        Assert.That(data[1].Icd10Code, Is.EqualTo("J18.9"));
        _repoMock.Verify(r => r.Add(It.IsAny<DiseasePriorProbability>()), Times.Exactly(2));
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
