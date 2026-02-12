using NUnit.Framework;
using SuperSpinner.Models;
using SuperSpinner.Services;
using System.Threading.Tasks;
using UnityEngine;

namespace SuperSpinner.Tests
{
    public class MockWebRequestHandler : IWebRequestHandler
    {
        public string MockResponseJson { get; set; }
        public bool ShouldFail { get; set; }
        public bool ShouldTimeout { get; set; }
        public string LastGetUrl { get; private set; }
        public string LastPostUrl { get; private set; }
        public int GetCallCount { get; private set; }
        public int PostCallCount { get; private set; }
        public int LastTimeout { get; private set; }

        public Task<string> GetAsync(string url, int timeout)
        {
            GetCallCount++;
            LastGetUrl = url;
            LastTimeout = timeout;
            
            if (ShouldFail || ShouldTimeout)
                return Task.FromResult<string>(null);
            
            return Task.FromResult(MockResponseJson);
        }

        public Task<string> PostAsync(string url, int timeout)
        {
            PostCallCount++;
            LastPostUrl = url;
            LastTimeout = timeout;
            
            if (ShouldFail || ShouldTimeout)
                return Task.FromResult<string>(null);
            
            return Task.FromResult(MockResponseJson);
        }
    }

    [TestFixture]
    public class SpinnerServiceTests
    {
        private SpinnerService _service;
        private MockWebRequestHandler _mockWebHandler;

        [SetUp]
        public void SetUp()
        {
            GameObject serviceObject = new GameObject("TestSpinnerService");
            _service = serviceObject.AddComponent<SpinnerService>();
            
            // Inject mock web request handler
            _mockWebHandler = new MockWebRequestHandler();
            _service.SetWebRequestHandler(_mockWebHandler);
            
            // Set a test API URL
            _service.GetType()
                .GetField("_cachedUrl", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_service, "https://test-api.com/");
        }

        [TearDown]
        public void TearDown()
        {
            if (_service != null)
                Object.DestroyImmediate(_service.gameObject);
        }

        [Test]
        public async Task GetSpinnerValues_WithValidResponse_ParsesJsonCorrectly()
        {
            // Arrange
            string jsonResponse = "{\"spinnerValues\":[100,500,1000,5000,10000]}";
            _mockWebHandler.MockResponseJson = jsonResponse;

            // Act
            var result = await _service.GetSpinnerValues();

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(5, result.spinnerValues.Length, "Should have 5 values");
            Assert.AreEqual(100, result.spinnerValues[0], "First value should be 100");
            Assert.AreEqual(10000, result.spinnerValues[4], "Last value should be 10000");
            Assert.AreEqual(1, _mockWebHandler.GetCallCount, "GET should be called once");
            Assert.IsTrue(_mockWebHandler.LastGetUrl.Contains("spinner/values"), "Should call correct endpoint");
        }

        [Test]
        public async Task GetSpinnerValues_WhenRequestFails_ReturnsNull()
        {
            // Arrange
            _mockWebHandler.ShouldFail = true;

            // Act
            var result = await _service.GetSpinnerValues();

            // Assert
            Assert.IsNull(result, "Result should be null when request fails");
            Assert.AreEqual(1, _mockWebHandler.GetCallCount, "GET should still be called");
        }

        [Test]
        public async Task GetSpinnerResult_WithValidResponse_ParsesJsonCorrectly()
        {
            // Arrange
            string jsonResponse = "{\"spinnerValue\":5000}";
            _mockWebHandler.MockResponseJson = jsonResponse;

            // Act
            var result = await _service.GetSpinnerResult();

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(5000, result.spinnerValue, "Spinner value should be 5000");
            Assert.AreEqual(1, _mockWebHandler.PostCallCount, "POST should be called once");
            Assert.IsTrue(_mockWebHandler.LastPostUrl.Contains("spinner/spin"), "Should call correct endpoint");
        }

        [Test]
        public async Task GetSpinnerResult_WhenRequestFails_ReturnsNull()
        {
            // Arrange
            _mockWebHandler.ShouldFail = true;

            // Act
            var result = await _service.GetSpinnerResult();

            // Assert
            Assert.IsNull(result, "Result should be null when request fails");
            Assert.AreEqual(1, _mockWebHandler.PostCallCount, "POST should still be called");
        }

        [Test]
        public async Task GetSpinnerResult_WithEmptyResponse_ReturnsNull()
        {
            // Arrange
            _mockWebHandler.MockResponseJson = "";

            // Act
            var result = await _service.GetSpinnerResult();

            // Assert
            Assert.IsNull(result, "Result should be null with empty response");
        }

        [Test]
        public async Task GetSpinnerValues_CallsCorrectEndpoint()
        {
            // Arrange
            _mockWebHandler.MockResponseJson = "{\"spinnerValues\":[100]}";

            // Act
            await _service.GetSpinnerValues();

            // Assert
            Assert.AreEqual("https://test-api.com/spinner/values", _mockWebHandler.LastGetUrl, 
                "Should call the correct API endpoint");
        }

        [Test]
        public async Task GetSpinnerResult_CallsCorrectEndpoint()
        {
            // Arrange
            _mockWebHandler.MockResponseJson = "{\"spinnerValue\":1000}";

            // Act
            await _service.GetSpinnerResult();

            // Assert
            Assert.AreEqual("https://test-api.com/spinner/spin", _mockWebHandler.LastPostUrl, 
                "Should call the correct API endpoint");
        }

        [Test]
        public async Task GetSpinnerValues_WhenTimeout_ReturnsNull()
        {
            // Arrange
            _mockWebHandler.ShouldTimeout = true;

            // Act
            var result = await _service.GetSpinnerValues();

            // Assert
            Assert.IsNull(result, "Result should be null when request times out");
            Assert.AreEqual(1, _mockWebHandler.GetCallCount, "GET should be called once");
            Assert.AreEqual(10, _mockWebHandler.LastTimeout, "Timeout should be set to 10 seconds");
        }

        [Test]
        public async Task GetSpinnerResult_WhenTimeout_ReturnsNull()
        {
            // Arrange
            _mockWebHandler.ShouldTimeout = true;

            // Act
            var result = await _service.GetSpinnerResult();

            // Assert
            Assert.IsNull(result, "Result should be null when request times out");
            Assert.AreEqual(1, _mockWebHandler.PostCallCount, "POST should be called once");
            Assert.AreEqual(10, _mockWebHandler.LastTimeout, "Timeout should be set to 10 seconds");
        }
    }
}
