using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Controllers;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PromoCodeFactory.UnitTests.WebHost.Controllers.Partners
{
    public class SetPartnerPromoCodeLimitAsyncTests
    {
        //TODO: Add Unit Tests
        private readonly Mock<IRepository<Partner>> _partnersRepositoryMock;
        private readonly PartnersController _partnersController;
        private readonly Fixture _fixture;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SetPartnerPromoCodeLimitAsyncTests()
        {
            _partnersRepositoryMock = new Mock<IRepository<Partner>>();
            _partnersController = new PartnersController(_partnersRepositoryMock.Object);
            _fixture = new Fixture();
        }

        /// <summary>
        /// Фабричный метод для создания Partner
        /// </summary>
        /// <param name="setup">Делегат для изменения свойств экземпляра Partner</param>
        /// <returns></returns>
        private Partner BuildPartner(Action<Partner> setup = null)
        {
            var partner = new Partner
            {
                Id = Guid.NewGuid(),
                Name = "Test Partner",
                IsActive = true,
                NumberIssuedPromoCodes = 5,
                PartnerLimits = new List<PartnerPromoCodeLimit>()
            };
            setup?.Invoke(partner);
            return partner;
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_Should_Throw_Exception_If_Partner_Is_NotFound_Return_404()
        {
            // Arrange
            var partnerId = Guid.NewGuid();
            var request = _fixture.Create<SetPartnerPromoCodeLimitRequest>();

            // Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(partnerId, request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_Should_Throw_Exception_If_Partner_Is_NotActive_Return_400()
        {
            // Arrange
            var partner = BuildPartner(p => p.IsActive = false);
            _partnersRepositoryMock.Setup(r => r.GetByIdAsync(partner.Id)).ReturnsAsync(partner);

            var request = _fixture.Create<SetPartnerPromoCodeLimitRequest>();

            // Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}