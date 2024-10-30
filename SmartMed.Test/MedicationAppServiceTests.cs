using FluentValidation;
using FluentValidation.Results;
using Moq;
using SmartMed.Application.Medications;
using SmartMed.Application.Medications.DTOs;
using SmartMed.Application.Medications.Mappers;
using SmartMed.Domain.Medications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartMed.Test
{
    public class MedicationAppServiceTests
    {
        private readonly Mock<IMedicationRepository> _medicationRepositoryMock;
        private readonly Mock<IMedicationValidator> _medicationValidatorMock;
        private readonly Mock<IMedicationMapper> _medicationMapperMock;
        private readonly MedicationAppService _medicationAppService;

        public MedicationAppServiceTests()
        {
            _medicationRepositoryMock = new Mock<IMedicationRepository>();
            _medicationValidatorMock = new Mock<IMedicationValidator>();
            _medicationMapperMock = new Mock<IMedicationMapper>();
            _medicationAppService = new MedicationAppService(
                _medicationRepositoryMock.Object,
                _medicationValidatorMock.Object,
                _medicationMapperMock.Object);
        }

        [Fact]
        public async Task AddAsync_ValidMedication_AddsMedication()
        {
            // Arrange
            var medicationDto = new MedicationDto { Name = "Acetaminophen", Quantity = 5 };
            var medication = new Medication { Name = "Acetaminophen", Quantity = 5 };
            var validationResult = new ValidationResult { };

            _medicationMapperMock.Setup(m => m.ToDomain(medicationDto)).Returns(medication);
            _medicationValidatorMock.Setup(v => v.Validate(medication)).Returns(validationResult);
            _medicationRepositoryMock.Setup(r => r.AddAsync(medication)).Returns(Task.CompletedTask);

            // Act
            await _medicationAppService.AddAsync(medicationDto);

            // Assert
            _medicationRepositoryMock.Verify(r => r.AddAsync(medication), Times.Once);
        }

        [Fact]
        public async Task AddAsync_InvalidMedication_ThrowsValidationException()
        {
            // Arrange
            var medicationDto = new MedicationDto {  Quantity = 5 };
            var medication = new Medication {  Quantity = 5 };
            var validationResult = new ValidationResult
            {
                Errors = new List<ValidationFailure> { new ValidationFailure(nameof(MedicationDto.Name), "Invalid medication") }
            };

            _medicationMapperMock.Setup(m => m.ToDomain(medicationDto)).Returns(medication);
            _medicationValidatorMock.Setup(v => v.Validate(medication)).Returns(validationResult);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _medicationAppService.AddAsync(medicationDto));
            Assert.Equal(validationResult.Errors, exception.Errors);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_DeletesMedication()
        {
            // Arrange
            int medicationId = 1;
            _medicationRepositoryMock.Setup(r => r.DeleteAsync(medicationId)).ReturnsAsync(true);

            // Act
            var result = await _medicationAppService.DeleteAsync(medicationId);

            // Assert
            Assert.True(result);
            _medicationRepositoryMock.Verify(r => r.DeleteAsync(medicationId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMedicationList()
        {
            // Arrange
            var medications = new List<Medication>
            {
                new Medication { Name = "Acetaminophen", Quantity = 5 },
                new Medication { Name = "Ibuprofen", Quantity = 20 }
            };

            var medicationListDto = new List<MedicationListDto>
            {
                new MedicationListDto { Name = "Acetaminophen", Quantity = 5 },
                new MedicationListDto { Name = "Ibuprofen", Quantity = 20 }
            };

            _medicationRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(medications);
            _medicationMapperMock.Setup(m => m.ToListDto(It.IsAny<Medication>())).Returns((Medication m) => new MedicationListDto { Name = m.Name, Quantity = m.Quantity });

            // Act
            var result = await _medicationAppService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Acetaminophen", result.First().Name);
            Assert.Equal("Ibuprofen", result.Last().Name);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsMedicationDto()
        {
            // Arrange
            int medicationId = 1;
            var medication = new Medication { Name = "Acetaminophen", Quantity = 5 };
            var medicationDto = new MedicationDto { Name = "Acetaminophen", Quantity = 5 };

            _medicationRepositoryMock.Setup(r => r.GetByIdAsync(medicationId)).ReturnsAsync(medication);
            _medicationMapperMock.Setup(m => m.ToDto(medication)).Returns(medicationDto);

            // Act
            var result = await _medicationAppService.GetByIdAsync(medicationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Acetaminophen", result.Name);
            Assert.Equal(5, result.Quantity);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            int medicationId = 1;
            _medicationRepositoryMock.Setup(r => r.GetByIdAsync(medicationId)).ReturnsAsync((Medication)null);

            // Act
            var result = await _medicationAppService.GetByIdAsync(medicationId);

            // Assert
            Assert.Null(result);
        }
    }
}