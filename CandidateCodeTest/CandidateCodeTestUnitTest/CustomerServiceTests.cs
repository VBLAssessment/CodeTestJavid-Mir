using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Services;
using CandidateCodeTest.Services.Implementations;
using Moq;

namespace CandidateCodeTest
{
    // Write the following unit tests
    public class CustomerServiceTests
    {
        private CustomerService _customerService;
        private MessageService messageService;
        private Mock<IMessageService>? _messageService;
        private readonly Mock<ILogWriter> _logWriter;

        public CustomerServiceTests()
        {
            //creating mock
            _messageService = new Mock<IMessageService>();
            _logWriter = new Mock<ILogWriter>();
        }

        [Fact]
        public async Task Within_Time_Window_Email_Has_Been_Sent()
        {
            // Arrange
            _messageService?.Setup(m => m.SendEmailAsync());
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = new TimeSpan(23, 59, 59);
            _customerService = new CustomerService(_messageService?.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = await _customerService.HasEmailBeenSent();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Within_Time_Window_Email_Has_Been_Sent_MessageService()
        {
            // Arrange
            _messageService?.Setup(m => m.SendEmailAsync());
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = new TimeSpan(23, 59, 59);
            messageService = new MessageService(_logWriter.Object);

            // Act
            await messageService.SendEmailAsync();

            //// Assert
            //Assert.True(result);
        }

        [Fact]
        public async Task Outside_Time_Window_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService?.Setup(m => m.SendEmailAsync());
            var startTime = new TimeSpan();
            var endTime = new TimeSpan();
            _customerService = new CustomerService(_messageService?.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = await _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Throw_Exception_Email_Has_Not_Been_Sent()
        {
            // Arrange
            bool result = false;
            try
            {
                var startTime = new TimeSpan(0, 0, 0);
                var endTime = new TimeSpan(23, 59, 59);
                _messageService = null;
                _customerService = new CustomerService(null, startTime, endTime, null);

                // Act
                result = await _customerService.HasEmailBeenSent();
            }
            catch (Exception)
            {
                //assert
                await Assert.ThrowsAsync<NullReferenceException>(async () => await _customerService.HasEmailBeenSent());
            }
        }

        [Fact]
        public async Task Start_Date_Null_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService?.Setup(m => m.SendEmailAsync());
            var startTime = (TimeSpan?)null;
            var endTime = new TimeSpan(23, 59, 59);
            _customerService = new CustomerService(_messageService?.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = await _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task End_Date_Null_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService?.Setup(m => m.SendEmailAsync());
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = (TimeSpan?)null;
            _customerService = new CustomerService(_messageService?.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = await _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Null_Params_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService?.Setup(m => m.SendEmailAsync());
            var startTime = (TimeSpan?)null;
            var endTime = (TimeSpan?)null;
            _customerService = new CustomerService(_messageService?.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = await _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }
    }
}