using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Xunit;
using WA_Pharmacy.AppCode.Services;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.EFCore.UnitOfWork;
using WA_Pharmacy.EFCore.Entities;
using WA_Pharmacy.DTOs.Prescription;
using WA_Pharmacy.AppCode.Exceptions;
using AutoMapper;
using WA_Pharmacy.AppCode.Exceptions;

namespace WA_Pharmacy.Tests
{
    public class PrescriptionServiceTests
    {
        private readonly Mock<IGenericRepository<Prescription, long>> _mockPrescriptionRepo;
        private readonly Mock<IGenericRepository<Medicine, int>> _mockMedicineRepo;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PrescriptionService _prescriptionService;

        public PrescriptionServiceTests()
        {
            // Setup Mocks
            _mockPrescriptionRepo = new Mock<IGenericRepository<Prescription, long>>();
            _mockMedicineRepo = new Mock<IGenericRepository<Medicine, int>>();
            _mockUow = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();

            // Inject Mocked Dependencies
            _prescriptionService = new PrescriptionService(
                _mockPrescriptionRepo.Object,
                _mockMedicineRepo.Object,
                _mockUow.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task AddPrescriptionAsync_WithValidData_ReturnsPrescriptionAndSaves()
        {
            // Arrange
            var dto = new PrescriptionCreateDto
            {
                CustomerId = 1,
                DoctorId = 2,
                Items = new List<PrescriptionItemDto>
                {
                    new PrescriptionItemDto { MedicineId = 10, Quantity = 2 },
                    new PrescriptionItemDto { MedicineId = 20, Quantity = 1 }
                }
            };

            var medicine1 = new Medicine { Id = 10, MedicineName = "Med1", Stock = 10, Price = 1000m };
            var medicine2 = new Medicine { Id = 20, MedicineName = "Med2", Stock = 5, Price = 500m };

            _mockMedicineRepo.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(medicine1);
            _mockMedicineRepo.Setup(r => r.GetByIdAsync(20)).ReturnsAsync(medicine2);
            
            _mockPrescriptionRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Prescription, bool>>>()))
                                 .ReturnsAsync(false);

            // Act
            var result = await _prescriptionService.AddPrescriptionAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CustomerId);
            Assert.Equal(2, result.DoctorId);
            Assert.NotNull(result.TrackingCode);
            Assert.Equal(2, result.MedicineList.Count);
            
            // 2 * 1000 + 1 * 500 = 2500m
            Assert.Equal(2500m, result.TotalPrice);
            
            // Check stock memory deduction
            Assert.Equal(8, medicine1.Stock);
            Assert.Equal(4, medicine2.Stock);

            _mockPrescriptionRepo.Verify(r => r.AddAsync(It.IsAny<Prescription>()), Times.Once);
            _mockUow.Verify(u => u.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task AddPrescriptionAsync_MedicineNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            var dto = new PrescriptionCreateDto
            {
                CustomerId = 1,
                DoctorId = 2,
                Items = new List<PrescriptionItemDto>
                {
                    new PrescriptionItemDto { MedicineId = 99, Quantity = 1 }
                }
            };

            _mockMedicineRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Medicine)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _prescriptionService.AddPrescriptionAsync(dto));
                
            Assert.Contains("دارو با شناسه 99 یافت نشد", ex.Message);
            
            _mockPrescriptionRepo.Verify(r => r.AddAsync(It.IsAny<Prescription>()), Times.Never);
            _mockUow.Verify(u => u.SaveChangesAsync(default), Times.Never);
        }

        [Fact]
        public async Task AddPrescriptionAsync_InsufficientStock_ThrowsInsufficientStockException()
        {
            // Arrange
            var dto = new PrescriptionCreateDto
            {
                CustomerId = 1,
                DoctorId = 2,
                Items = new List<PrescriptionItemDto>
                {
                    new PrescriptionItemDto { MedicineId = 10, Quantity = 5 } // Requested: 5
                }
            };
            
            var medicine = new Medicine { Id = 10, MedicineName = "Med1", Stock = 2, Price = 1000m }; // Available: 2
            _mockMedicineRepo.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(medicine);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InsufficientStockException>(() => 
                _prescriptionService.AddPrescriptionAsync(dto));
                
            Assert.Equal("Med1", ex.MedicineName);
            Assert.Equal(5, ex.RequestedQuantity);
            Assert.Equal(2, ex.AvailableStock);

            _mockPrescriptionRepo.Verify(r => r.AddAsync(It.IsAny<Prescription>()), Times.Never);
            _mockUow.Verify(u => u.SaveChangesAsync(default), Times.Never);
        }

        [Fact]
        public async Task AddPrescriptionAsync_DuplicateTrackingCode_RegeneratesTrackingCode()
        {
            // Arrange
            var dto = new PrescriptionCreateDto
            {
                CustomerId = 1,
                DoctorId = 2,
                Items = new List<PrescriptionItemDto>() // Empty items for simplicity
            };

            _mockPrescriptionRepo.SetupSequence(r => r.ExistsAsync(It.IsAny<Expression<Func<Prescription, bool>>>()))
                                 .ReturnsAsync(true)
                                 .ReturnsAsync(false);

            // Act
            var result = await _prescriptionService.AddPrescriptionAsync(dto);

            // Assert
            Assert.NotNull(result);
            _mockPrescriptionRepo.Verify(r => r.ExistsAsync(It.IsAny<Expression<Func<Prescription, bool>>>()), Times.Exactly(2));
            _mockPrescriptionRepo.Verify(r => r.AddAsync(It.IsAny<Prescription>()), Times.Once);
            _mockUow.Verify(u => u.SaveChangesAsync(default), Times.Once);
        }
    }
}
