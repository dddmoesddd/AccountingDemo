using AccountingVoucher.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VouchersTests.IntegerationTest
{
    public  class VoucherApiIntegerationTest:IClassFixture<WebApplicationFactory<Startup>>
    {
		private HttpClient Client;

		public VoucherApiIntegerationTest(WebApplicationFactory<Startup> factory)
		{
			Client = factory.CreateClient();
		}
		[Fact]
		public async Task TestGetStockItemsAsync()
		{
			// Arrange
			var request = "/api/Vocher/";

			// Act
			var response = await Client.GetAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
		}
	}
}
