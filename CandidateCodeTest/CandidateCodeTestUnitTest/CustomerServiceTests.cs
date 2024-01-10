using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Services;
using Moq;

namespace CandidateCodeTest
{
    // Write the following unit tests
    public class CustomerServiceTests
    {
        private readonly Mock<ILogWriter> _iLogWriterObj;
        private Mock<IMessageService>? _iMessageServiceObj;        
        private CustomerService _customerServiceObj;        

        public CustomerServiceTests()
        {
            //creating mock
            _iMessageServiceObj = new Mock<IMessageService>();
            _iLogWriterObj = new Mock<ILogWriter>();
        }

        [Fact]
        public async Task WithinTimeWindow_Email_Sent()
        {
            // Arrange
            _iMessageServiceObj?.Setup(m => m.SendEmailAsync());
            TimeSpan? startSendingTime = new TimeSpan(0, 0, 0);
            TimeSpan? endSendingTime = new TimeSpan(23, 59, 59);
            _customerServiceObj = new CustomerService(_iMessageServiceObj?.Object, startSendingTime, endSendingTime, _iLogWriterObj.Object);

            // Act
            bool isMailSent = await _customerServiceObj.HasEmailBeenSent();

            // Assert
            Assert.True(isMailSent);
        }

        [Fact]
        public async Task OutsideTimeWindow_Email_Not_Sent()
        {
            // Arrange
            _iMessageServiceObj?.Setup(m => m.SendEmailAsync());
            TimeSpan? startSendingTime = new TimeSpan();
            TimeSpan? endSendingTime = new TimeSpan();
            _customerServiceObj = new CustomerService(_iMessageServiceObj?.Object, startSendingTime, endSendingTime, _iLogWriterObj.Object);

            // Act
            bool isMailSent = await _customerServiceObj.HasEmailBeenSent();

            // Assert
            Assert.False(isMailSent);
        }
        [Fact]
        public async Task NullParams_Email_Not_Sent()
        {
            // Arrange
            _iMessageServiceObj?.Setup(m => m.SendEmailAsync());
            TimeSpan? startSendingTime = (TimeSpan?)null;
            TimeSpan? endSendingTime = (TimeSpan?)null;
            _customerServiceObj = new CustomerService(_iMessageServiceObj?.Object, startSendingTime, endSendingTime, _iLogWriterObj.Object);

            // Act
            bool isMailSent = await _customerServiceObj.HasEmailBeenSent();

            // Assert
            Assert.False(isMailSent);
        }

        [Fact]
        public async Task ThrowException_Email_Not_Sent()
        {
            // Arrange
            bool isMailSent = false;
            try
            {
                TimeSpan? startSendingTime = new TimeSpan(0, 0, 0);
                TimeSpan? endSendingTime = new TimeSpan(23, 59, 59);
                _iMessageServiceObj = null;
                _customerServiceObj = new CustomerService(null, startSendingTime, endSendingTime, null);

                // Act
                isMailSent = await _customerServiceObj.HasEmailBeenSent();
            }
            catch (Exception)
            {
                //assert
                await Assert.ThrowsAsync<NullReferenceException>(async () => await _customerServiceObj.HasEmailBeenSent());
            }
        }

        [Fact]
        public async Task StartDateNull_Email_Not_Sent()
        {
            // Arrange
            _iMessageServiceObj?.Setup(m => m.SendEmailAsync());
            TimeSpan? startSendingTime = (TimeSpan?)null;
            TimeSpan? endSendingTime = new TimeSpan(23, 59, 59);
            _customerServiceObj = new CustomerService(_iMessageServiceObj?.Object, startSendingTime, endSendingTime, _iLogWriterObj.Object);

            // Act
            bool isMailSent = await _customerServiceObj.HasEmailBeenSent();

            // Assert
            Assert.False(isMailSent);
        }

        [Fact]
        public async Task EndDateNull_Email_Not_Sent()
        {
            // Arrange
            _iMessageServiceObj?.Setup(m => m.SendEmailAsync());
            TimeSpan? startSendingTime = new TimeSpan(0, 0, 0);
            TimeSpan? endSendingTime = (TimeSpan?)null;
            _customerServiceObj = new CustomerService(_iMessageServiceObj?.Object, startSendingTime, endSendingTime, _iLogWriterObj.Object);

            // Act
            bool isMailSent = await _customerServiceObj.HasEmailBeenSent();

            // Assert
            Assert.False(isMailSent);
        }
    }
}