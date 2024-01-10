using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Services;
using CandidateCodeTest.Services.Implementations;
using Moq;

namespace CandidateCodeTest
{
    // Write the following unit tests
    public class MessageServiceTests
    {
        private readonly Mock<ILogWriter> _iLogWriterObj;
        private MessageService messageServiceObj;
        private Mock<IMessageService>? _iMessageServiceObj;

        public MessageServiceTests()
        {
            //creating mock
            _iMessageServiceObj = new Mock<IMessageService>();
            _iLogWriterObj = new Mock<ILogWriter>();
        }

        [Fact]
        public async Task Email_Sent_MessageService()
        {
            // Arrange
            _iMessageServiceObj?.Setup(m => m.SendEmailAsync());
            messageServiceObj = new MessageService(_iLogWriterObj.Object);

            // Act
            await messageServiceObj.SendEmailAsync();

            //// Assert
            //Assert.True(result);
        }

        [Fact]
        public async Task ThrowException_Email_Not_Sent_MessageService()
        {
            // Arrange
            try
            {
                messageServiceObj = new MessageService(null);
            }
            catch (Exception)
            {
                //act and assert
                await Assert.ThrowsAsync<NullReferenceException>(async () => await messageServiceObj.SendEmailAsync());
            }
        }
    }

  }